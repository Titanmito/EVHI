using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayNextLevel : MonoBehaviour
{
    public Button button;
    public void Play()
    {
        SceneManager.LoadScene("Level" + (PlayerPrefs.GetInt("Level") + 1).ToString(), LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync(gameObject.scene);
    }

    public void Start()
    {
        if (PlayerPrefs.GetInt("Level") != GlobalVariables.levelCount)
            button.interactable = true;
        else
            button.interactable = false;
    }
}
