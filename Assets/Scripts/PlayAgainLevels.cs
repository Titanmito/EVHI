using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayAgainLevels : MonoBehaviour
{

    public int level;

    public void playAgain(){
        if (PlayerPrefs.GetInt("Level") == 1 || level == 1)
        {
            SceneManager.LoadScene("Level1", LoadSceneMode.Additive);
            SceneManager.UnloadSceneAsync(gameObject.scene);
        }
        else if (PlayerPrefs.GetInt("Level") == 2)
        {
            SceneManager.LoadScene("Level2", LoadSceneMode.Additive);
            SceneManager.UnloadSceneAsync(gameObject.scene);
        }
        else if (PlayerPrefs.GetInt("Level") == 3)
        {
            SceneManager.LoadScene("Level3", LoadSceneMode.Additive);
            SceneManager.UnloadSceneAsync(gameObject.scene);
        }
    }
}
