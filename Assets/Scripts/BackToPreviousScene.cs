using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToPreviousScene : MonoBehaviour
{
    public string PreviousScene;
    public void Back()
    {
        SceneManager.LoadScene(PreviousScene, LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync(gameObject.scene);
    }
}
