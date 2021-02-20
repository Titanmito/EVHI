using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class RandomNotesGenerator : MonoBehaviour
{

    public GameObject notesPrefab;
    private float reloadTime;
    private float reloadProgress = 0f;
    private float randomX;
    public System.Random r = new System.Random();
    private float[] xPosition;
    private Color[] notesColor;
    private GameObject newObject;
    private int rand;
    private int count;
    private int i;
    private List<GlobalVariables.JoystickButton> RealButtons;
    private static readonly Dictionary<int, GlobalVariables.JoystickButton> MapRandomXToJoystickButton = new Dictionary<int, GlobalVariables.JoystickButton>
    {
        {0, GlobalVariables.JoystickButton.L1},
        {1, GlobalVariables.JoystickButton.Square},
        {2, GlobalVariables.JoystickButton.Cross},
        {3, GlobalVariables.JoystickButton.Triangle},
        {4, GlobalVariables.JoystickButton.Circle},
        {5, GlobalVariables.JoystickButton.R1}
    };

    // Start is called before the first frame update
    void Start()
    {
        try
        {
            LevelDifficulty difficulty;
            if (PlayerPrefs.GetInt("Level") != 0)
                difficulty = GlobalVariables.levelDifficulties[Tuple.Create(
                    PlayerPrefs.GetInt("Level"),
                    GameObject.Find(GlobalVariables.goNameUsersManager).GetComponent<ManageUsers>().currentUser.Aptitude)];
            else
                difficulty = GlobalVariables.levelDifficulties[Tuple.Create(0, (ushort)0)];
            reloadTime = difficulty.noteReloadTime;
            count = difficulty.noteCount;
            xPosition = new float[6] { -2.5f, -1.5f, -0.5f, 0.5f, 1.5f, 2.5f };
            notesColor = new Color[6] { Color.yellow, Color.magenta, Color.blue, Color.green, Color.red, Color.cyan };
            i = 0;
            RealButtons = GameObject.Find(GlobalVariables.goNameButtonPushContainer).GetComponent<ButtonPushContainer>().RealButtons;
            RealButtons.Clear();
        }
        catch(NullReferenceException e) { Debug.Log(e.Message); }
    }

    // Update is called once per frame
    void Update()
    {
        reloadProgress += Time.deltaTime;
        if(reloadProgress >= reloadTime && i < count){
            // Debug.Log(i);
            rand = r.Next(6);
            RealButtons.Add(MapRandomXToJoystickButton[rand]);
            randomX = xPosition[rand];
            newObject = Instantiate<GameObject>(notesPrefab, new Vector2(randomX, 6), Quaternion.identity);
            newObject.GetComponent<SpriteRenderer>().color = notesColor[rand];
            reloadProgress = 0f;
            i++;
        }
    }
}
