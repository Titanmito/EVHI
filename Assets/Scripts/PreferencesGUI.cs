using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PreferencesGUI : MonoBehaviour
{
    public void Launch()
    {
        SceneManager.LoadScene("Preferences", LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync(gameObject.scene);
    }
}
