﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class CountNotes : MonoBehaviour
{
    [HideInInspector]
    public int Count = 1;
    // Start is called before the first frame update
    void Start()
    {
        LevelDifficulty difficulty;
        if (PlayerPrefs.GetInt("Level") != 0)
            difficulty = GlobalVariables.levelDifficulties[Tuple.Create(
                PlayerPrefs.GetInt("Level"),
                GameObject.Find(GlobalVariables.goNameUsersManager).GetComponent<ManageUsers>().currentUser.Aptitude)];
        else
            difficulty = GlobalVariables.levelDifficulties[Tuple.Create(0, (ushort)0)];
        Count = difficulty.noteCount;
    }

    // Update is called once per frame
    void Update()
    {
        if (Count <= 0)
        {
            GameObject.Find(GlobalVariables.goNameUsersManager).GetComponent<ManageUsers>().currentUser.CurrentLevel = Math.Max(
                GameObject.Find(GlobalVariables.goNameUsersManager).GetComponent<ManageUsers>().currentUser.CurrentLevel,
                Math.Min((ushort)(PlayerPrefs.GetInt("Level") + 1), GlobalVariables.levelCount));
            Debug.Log(GameObject.Find(GlobalVariables.goNameUsersManager).GetComponent<ManageUsers>().currentUser.CurrentLevel);
            SceneManager.LoadScene("Win", LoadSceneMode.Additive);
            SceneManager.UnloadSceneAsync(gameObject.scene);
        }
    }
}
