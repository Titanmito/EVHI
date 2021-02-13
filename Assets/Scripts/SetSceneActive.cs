using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SetSceneActive : MonoBehaviour
{
    void Start()
    {
        SceneManager.SetActiveScene(gameObject.scene);
    }
}
