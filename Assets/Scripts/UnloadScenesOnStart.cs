using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class UnloadScenesOnStart : MonoBehaviour
{
    private string[] scenesToUnload = new string[12]
    {
        "Level1", "Level2", "Level3", "LevelChoice", "Login", "Lose", "NewUserNameSaved",
        "Preferences", "SignIn", "SignUp", "TestLevel", "Win"
    };
    // Start is called before the first frame update
    void Start()
    {
        foreach(string scene in scenesToUnload)
            SceneManager.UnloadSceneAsync(SceneManager.GetSceneByName(scene));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
