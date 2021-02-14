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
        GameObject.Find(GlobalVariables.goNameUsersCreator).GetComponent<UsersCreator>().SaveNextUserName(input.text);
        GameObject.Find(GlobalVariables.goNameUsersManager).GetComponent<ManageUsers>().CreateNewUser();
        SceneManager.LoadScene("NewUserNameSaved", LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync(gameObject.scene);
    }
}
