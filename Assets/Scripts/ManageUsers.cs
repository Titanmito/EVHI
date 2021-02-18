using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class ManageUsers : MonoBehaviour
{
    [HideInInspector]
    public List<User> users;
    [HideInInspector]
    public User currentUser = null;
    // Start is called before the first frame update
    void Start()
    {
        users = new List<User>();
        CreateRegisteredUsers();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateNewUser()
    {
        currentUser = GameObject.Find(GlobalVariables.goNameUsersCreator).GetComponent<UsersCreator>().CreateNewUser();
        users.Add(currentUser);
        //foreach (User user in users)
        //    Debug.Log(user.ToString());
        //currentUser.DumpInitData();
        //currentUser.DumpToUsers();
        //currentUser.DumpToArchive();
    }

    void CreateRegisteredUsers()
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
}
