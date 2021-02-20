using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class SetSceneActive : MonoBehaviour
{
    void Start()
    {
        try { SceneManager.SetActiveScene(gameObject.scene); }
        catch(ArgumentException e) { Debug.Log(e.Message); }
    }
}
