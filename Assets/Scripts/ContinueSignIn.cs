using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ContinueSignIn : MonoBehaviour
{
    public GameObject DropDownManager;
    public void Continue()
    {
        var antd = DropDownManager.GetComponent<AddNamesToDropdown>();
        int userID = antd.MapOptionsToID[antd.dropdown.options[antd.dropdown.value]];
        List<User> users = GameObject.Find(GlobalVariables.goNameUsersManager).GetComponent<ManageUsers>().users;
        for (int i = 0; i < users.Count; i++)
        {
            if (users[i].UserID == userID)
            {
                GameObject.Find(GlobalVariables.goNameUsersManager).GetComponent<ManageUsers>().currentUser = GameObject.Find(GlobalVariables.goNameUsersManager).GetComponent<ManageUsers>().users[i];
                break;
            }
        }
        SceneManager.LoadScene("LevelChoice", LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync(gameObject.scene);
    }
}
