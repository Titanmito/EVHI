using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayAgainTest : MonoBehaviour
{
    public void playAgain(){
        SceneManager.LoadScene("TestLevel");
    }
}
