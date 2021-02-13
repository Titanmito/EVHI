using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SignInGUI : MonoBehaviour
{
    public void Launch()
    {
        SceneManager.LoadScene("SignIn", LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync(gameObject.scene);
    }
}
