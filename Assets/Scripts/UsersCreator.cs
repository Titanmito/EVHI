using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsersCreator : MonoBehaviour
{
    [HideInInspector]
    public string nextUserName;
    [HideInInspector]
    public int nextUserID;

    private ushort defaultUserLevel = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SaveNextUserName(string name)
    {
        nextUserName = name;
        nextUserID = GenerateUserID();
        // Debug.Log("Saved! Name: " + name + " id: " + nextUserID.ToString());
    }

    public int GenerateUserID()
    {
        return GlobalVariables.GetMaxUserID() + 1;
        // return Random.Range(0, 100);
    }

    public User CreateNewUser()
    {
        return new User(nextUserID, nextUserName, currentLevel: defaultUserLevel);
    }
}
