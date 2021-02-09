using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class KillZone : MonoBehaviour
{

    int multiplier = 1;
    int streak = 0;

    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("Score", 0);
        PlayerPrefs.SetInt("Streak", 0);
        PlayerPrefs.SetInt("RockMetter", 25);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col){
        Destroy(col.gameObject);
        ResetStreak();
    }

    public void ResetStreak(){
        PlayerPrefs.SetInt("RockMetter", PlayerPrefs.GetInt("RockMetter") - 2);
        if (PlayerPrefs.GetInt("RockMetter") < 0){
            Lose();
        }
        streak = 0;
        multiplier = 1;
        UpdateGUI();
    }

    public void AddStreak(){
        if (PlayerPrefs.GetInt("RockMetter") + 1 <= 50 ){
            PlayerPrefs.SetInt("RockMetter", PlayerPrefs.GetInt("RockMetter") + 1);
        }
        streak++;
        multiplier = Math.Min(4, (streak/8)+1);
        multiplier = Math.Max(multiplier, 1);
        UpdateGUI();
    }

    void Lose(){
        SceneManager.LoadScene("Loose");
    }

    void UpdateGUI(){
        PlayerPrefs.SetInt("Streak", streak);
        PlayerPrefs.SetInt("Mult", multiplier);
    }

    public int GetScore(){
        return 100 * multiplier;
    }
}
