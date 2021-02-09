using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayAgain : MonoBehaviour
{
    public void playAgain(){
        SceneManager.LoadScene("SampleScene");
    }
}
