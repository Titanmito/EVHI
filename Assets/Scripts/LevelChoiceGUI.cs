using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChoiceGUI : MonoBehaviour
{
    public void Launch()
    {
        SceneManager.LoadScene("LevelChoice", LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync(gameObject.scene);
    }
}
