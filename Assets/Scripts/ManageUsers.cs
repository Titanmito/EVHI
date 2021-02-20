using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Linq;
using System.IO;
using System.Globalization;

public class ManageUsers : MonoBehaviour
{
    [HideInInspector]
    public List<User> users;
    [HideInInspector]
    public User currentUser = null;
    [HideInInspector]
    public float currentUserTypingTime = -1f;

    private List<GlobalVariables.JoystickButton> RealButtons = null, PushedButtons = null;
    private List<float> RealMoments = null, PushedMoments = null;
    private GlobalVariables.JoystickButton[] joystickButtons;
    private bool mptUpdate = true;
    private float mptUpdateProgress = 10.0f, mptUpdateReload = 10.0f;
    private string[] levelsNames;
    private bool waitForAptitude = false;
  
    // Start is called before the first frame update
    void Start()
    {
        users = new List<User>();
        CreateRegisteredUsers();
        joystickButtons = (GlobalVariables.JoystickButton[])Enum.GetValues(typeof(GlobalVariables.JoystickButton));
        levelsNames = new string[GlobalVariables.levelCount + 1];
        for (int i = 0; i < GlobalVariables.levelCount + 1; i++)
            levelsNames[i] = i == 0 ? "TestLevel" : ("Level" + i.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        mptUpdateProgress += Time.deltaTime;
        if (mptUpdate && mptUpdateProgress >= mptUpdateReload && !Array.Exists(levelsNames, name => name == SceneManager.GetActiveScene().name))
        {
            UpdateMentalPointingTyping();
            mptUpdate = false;
            mptUpdateProgress = 0.0f;
        }
        if (waitForAptitude)
        {
            string aptStr = File.ReadAllText(GlobalVariables.lastAptitudeReply);
            int aptitudeInt;
            if (!Int32.TryParse(aptStr, out aptitudeInt))
                return;
            ushort aptitude = (ushort)aptitudeInt;
            Debug.Log("Aptitude received: " + aptitude);
            currentUser.Aptitude = aptitude;
            using (FileStream fs = new FileStream(GlobalVariables.lastAptitudeReply, FileMode.Create, FileAccess.Write))
                using (StreamWriter sw = new StreamWriter(fs))
                    sw.Write("");
            waitForAptitude = false;
        }
    }

    public void CreateNewUser()
    {
        currentUser = GameObject.Find(GlobalVariables.goNameUsersCreator).GetComponent<UsersCreator>().CreateNewUser();
        users.Add(currentUser);
        if (currentUserTypingTime >= 0f)
        {
            // Debug.Log("I do a trace for user " + currentUser.Name + " with typing time: " + currentUserTypingTime.ToString());
            DumpMentalPointingTyping(typing: currentUserTypingTime);
            currentUserTypingTime = -1f;
        }
        currentUser.DumpToUsers();
    }

    public void CreateRegisteredUsers()
    {
        string[] lines = File.ReadAllLines(GlobalVariables.usersPath);
        string line = "";
        string[] csvValues;
        int id = 0;
        string name = "";
        ushort currentLevel = 0, aptitude = 0;
        double[] features = new double[GlobalVariables.nbFeatures], initFeatures = new double[GlobalVariables.nbFeatures];

        for (int i = 1; i < lines.Length; i++)
        {
            line = lines[i];
            if (line != "")
            {
                csvValues = line.Split(GlobalVariables.csvValueSeparator[0]);

                id = Int32.Parse(csvValues[GlobalVariables.csvUsersIDIndex]);
                name = csvValues[GlobalVariables.csvUsersNameIndex];
                currentLevel = UInt16.Parse(csvValues[GlobalVariables.csvUsersCurrentLevelIndex]);
                aptitude = UInt16.Parse(csvValues[GlobalVariables.csvUsersAptitudeIndex]);
                for (int k = GlobalVariables.csvUsersFeaturesFirstIndex, j = 0;
                    k < GlobalVariables.csvUsersFeaturesLastIndex && j < GlobalVariables.nbFeatures; k++, j++)
                    features[j] = Double.Parse(csvValues[k]);
                users.Add(new User(id, name, features, aptitude, currentLevel: currentLevel));
            }
        }

        for (int i = 0; i < users.Count; i++)
        {
            lines = File.ReadAllLines(GlobalVariables.initPath);
            for (int indexLine = 1; indexLine < lines.Length; indexLine++)
            {
                line = lines[indexLine];
                if (line != "")
                {
                    csvValues = line.Split(GlobalVariables.csvValueSeparator[0]);
                    id = Int32.Parse(csvValues[GlobalVariables.csvInitIDIndex]);
                    if (users[i].UserID == id)
                    {
                        users[i].InitAptitude = UInt16.Parse(csvValues[GlobalVariables.csvInitAptitudeIndex]);
                        for (int k = GlobalVariables.csvInitFeaturesFirstIndex, j = 0;
                            k < GlobalVariables.csvInitFeaturesLastIndex && j < GlobalVariables.nbFeatures; k++, j++)
                            initFeatures[j] = Double.Parse(csvValues[k]);
                        users[i].InitFeatures = initFeatures;
                    }
                }
            }
        }
    }

    public void DumpMentalPointingTyping(float mental = -1f, float pointing = -1f, float typing = -1f)
    {
        string line = currentUser.UserID.ToString() + GlobalVariables.csvValueSeparator;
        if (mental >= 0f)
            line += mental.ToString();
        line += GlobalVariables.csvValueSeparator;
        if (pointing >= 0f)
            line += pointing.ToString();
        line += GlobalVariables.csvValueSeparator;
        if (typing >= 0f)
            line += typing.ToString();
        DateTime utc = DateTime.UtcNow;
        line += GlobalVariables.csvValueSeparator + utc.ToString(new CultureInfo(GlobalVariables.cultureName));
        using (FileStream fs = new FileStream(GlobalVariables.mentalPointingTypingPath, FileMode.Append, FileAccess.Write))
            using (StreamWriter sw = new StreamWriter(fs))
                sw.WriteLine(line);
        mptUpdate = true;
    }

    public void DumpButtonPushes(bool testLevel = false)
    {
        UpdateButtonPushes(testLevel);
    }

    public void UpdateMentalPointingTyping()
    {
        if (currentUser == null)
            return;
        int[] counts = new int[GlobalVariables.nbExtraFeatures];
        double[] times = new double[GlobalVariables.nbExtraFeatures];
        for (int j = 0; j < GlobalVariables.nbExtraFeatures; j++)
        {
            counts[j] = 0;
            times[j] = 0.0;
        }
        double[] vect = new double[GlobalVariables.nbFeatures];
        int id = 0;
        string[] lines = File.ReadAllLines(GlobalVariables.mentalPointingTypingPath), lineSplit;
        string line = "";

        for (int i = 1; i < lines.Length; i++)
        {
            line = lines[i];
            if (line != "")
            {
                lineSplit = line.Split(GlobalVariables.csvValueSeparator[0]);
                id = Int32.Parse(lineSplit[0]);
                if (id == currentUser.UserID)
                {
                    for (int j = 1; j < ((int)GlobalVariables.nbExtraFeatures) + 1; j++)
                    {
                        if (lineSplit[j] != "")
                        {
                            times[j - 1] += Double.Parse(lineSplit[j]);
                            counts[j - 1]++;
                        }
                    }
                }
            }
        }
        for (int j = 0; j < GlobalVariables.nbExtraFeatures; j++)
            if (counts[j] != 0)
                times[j] /= counts[j];
        for (
            int i = GlobalVariables.nbFeatures - ((int)GlobalVariables.nbExtraFeatures), j = 0;
            i < GlobalVariables.nbFeatures && j < GlobalVariables.nbExtraFeatures; i++, j++
            )
            vect[i] = times[j];
        for (int i = 0; i < GlobalVariables.nbFeatures - ((int)GlobalVariables.nbExtraFeatures); i++)
            vect[i] = currentUser.Features[i];
        currentUser.Features = vect;
        UpdateAptitude();
        Debug.Log("Dumping MPT");
        currentUser.DumpToUsers();
        currentUser.DumpToArchive();
    }

    public void UpdateButtonPushes(bool testLevel = false)
    {
        if (currentUser == null)
            return;
        if (RealMoments == null)
            RealMoments = GameObject.Find(GlobalVariables.goNameButtonPushContainer).GetComponent<ButtonPushContainer>().RealMoments;
        if (RealButtons == null)
            RealButtons = GameObject.Find(GlobalVariables.goNameButtonPushContainer).GetComponent<ButtonPushContainer>().RealButtons;
        if (PushedMoments == null)
            PushedMoments = GameObject.Find(GlobalVariables.goNameButtonPushContainer).GetComponent<ButtonPushContainer>().PushedMoments;
        if (PushedButtons == null)
            PushedButtons = GameObject.Find(GlobalVariables.goNameButtonPushContainer).GetComponent<ButtonPushContainer>().PushedButtons;
        if (RealMoments.Count != RealButtons.Count || PushedMoments.Count != PushedButtons.Count)
            Debug.Log("Warning: pushes data desynchronization!");
        int minCount = Math.Min(RealMoments.Count, PushedMoments.Count);
        int buttonIndex, buttonIndex1;
        double[] vect = new double[GlobalVariables.nbFeatures];
        for (int i = 0; i < GlobalVariables.nbFeatures; i++)
            vect[i] = 0.0;
        int[] counts = new int[GlobalVariables.keysCount];
        for (int i = 0; i < GlobalVariables.keysCount; i++)
            counts[i] = 0;
        for (int i = 0; i < minCount; i++)
        {
            buttonIndex = JoystickButtonIndex(RealButtons[i]);
            counts[buttonIndex]++;
            vect[buttonIndex] += PushedMoments[i] - RealMoments[i];
        }
        for (int i = 0; i < GlobalVariables.keysCount; i++)
        {
            if (counts[i] != 0)
                vect[i] /= counts[i];
            else
                vect[i] = currentUser.Features[i];
        }
        vect[GlobalVariables.keysCount] = RealMoments.Count - PushedMoments.Count;

        for (int i = 0; i < minCount; i++)
        {
            buttonIndex = JoystickButtonIndex(RealButtons[i]);
            buttonIndex1 = JoystickButtonIndex(PushedButtons[i]);
            vect[GlobalVariables.keysCount + 1 + ((int)GlobalVariables.keysCount) * buttonIndex + buttonIndex1] += 1;
        }
        for (int i = GlobalVariables.nbFeatures - ((int)GlobalVariables.nbExtraFeatures); i < GlobalVariables.nbFeatures; i++)
            vect[i] = currentUser.Features[i];
        currentUser.Features = vect;
        if (testLevel)
            currentUser.InitFeatures = vect;

        // For debug
        //string s = "";
        //foreach (double v in vect)
        //    s += v.ToString() + " ";
        //Debug.Log(s);

        UpdateAptitude(testLevel);
        if (testLevel)
            currentUser.InitAptitude = currentUser.Aptitude;
        Debug.Log("Dumping button pushes");
        currentUser.DumpToUsers();
        currentUser.DumpToArchive();
        if (testLevel)
            currentUser.DumpInitData();
    }

    public void UpdateAptitude(bool testLevel = false)
    {
        if (testLevel)
        {
            int SuccessfulPushes = (int)GameObject.Find(GlobalVariables.goNameButtonPushContainer).GetComponent<ButtonPushContainer>().SuccessfulPushes;
            int maxCount = Math.Max(PushedButtons.Count, RealButtons.Count);
            double a = (SuccessfulPushes * 1.0) / maxCount;
            ushort aptitude = (ushort)0;
            if (0.25 <= a && a < 0.5)
                aptitude = (ushort)1;
            else if (0.5 <= a && a < 0.75)
                aptitude = (ushort)2;
            else if (0.75 <= a && a < 1)
                aptitude = (ushort)3;
            else if (SuccessfulPushes == maxCount)
                aptitude = (ushort)4;
            currentUser.Aptitude = aptitude;
        }
        else
        {
            using (FileStream fs = new FileStream(GlobalVariables.lastAptitudeRequest, FileMode.Create, FileAccess.Write))
                using (StreamWriter sw = new StreamWriter(fs))
                    sw.Write(currentUser.FeaturesToString(GlobalVariables.csvValueSeparator));
            Debug.Log("Features sent " + currentUser.FeaturesToString(GlobalVariables.csvValueSeparator));
            waitForAptitude = true;
        }
    }

    private int JoystickButtonIndex(GlobalVariables.JoystickButton button)
    {
        return System.Array.FindIndex(joystickButtons, btn => btn.Equals(button));
    }
}
