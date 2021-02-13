using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ConfirmNewUserReg : MonoBehaviour
{
    public InputField input;

    public void Confirm()
    {
        // Debug.Log(input.text);
        GameObject.Find("UsersCreator").GetComponent<UsersCreator>().SaveNextUserName(input.text);
        SceneManager.LoadScene("NewUserNameSaved", LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync(gameObject.scene);
    }
}
