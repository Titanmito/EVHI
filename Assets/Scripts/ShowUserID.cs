using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ShowUserID : MonoBehaviour
{
    public Text text;
    private string sepTextID = ":\n";
    // Start is called before the first frame update
    void Start()
    {
        text.text = text.text.Split(sepTextID[0])[0] + sepTextID + GameObject.Find("UsersCreator").GetComponent<UsersCreator>().nextUserID.ToString();
    }

}
