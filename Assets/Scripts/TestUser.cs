using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TestUser : MonoBehaviour
{
    private List<User> users;
    private double v = 0.5;

    // Start is called before the first frame update
    void Start()
    {
        User user0 = new User(0, "User0");
        User user1 = new User(1, "User1");
        User user2 = new User(2, "User2");

        users = new List<User>();
        users.Add(user0);
        users.Add(user1);
        users.Add(user2);

        foreach (User user in users)
        {
            try
            {
                user.DumpInitData();
            }
            catch (Exception e) { }
            user.DumpToArchive();
            user.DumpToUsers();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.U))
        {
            Debug.Log(v);
            for (int i = 0; i < users.Count; i++)
            {
                users[i].UpdateFeatures(v);
                users[i].DumpToArchive();
                users[i].DumpToUsers();
            }
            v += 0.5;
        }

    }
}
