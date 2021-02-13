using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SignUpGUI : MonoBehaviour
{
    public void Launch()
    {
        SceneManager.LoadScene("SignUp", LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync(gameObject.scene);
    }
}
