using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PlayLevelButtonClick : MonoBehaviour
{
    public Dropdown dropdown;
    public GameObject playLevelManager;
    public void PlayLevel()
    {
        string levelToPlay = dropdown.options[dropdown.value].text.Split(GlobalVariables.levelTitleSep[0])[1];
        if (levelToPlay == GlobalVariables.levelTitleTestPostfix)
            PlayerPrefs.SetInt("Level", 0);
        else
            PlayerPrefs.SetInt("Level", Int32.Parse(levelToPlay));
        playLevelManager.GetComponent<PlayAgainLevels>().playAgain();
    }
}
