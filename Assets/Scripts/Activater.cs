using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activater : MonoBehaviour
{

    public KeyCode key;
    private bool active = false;
    private GameObject note;
    private SpriteRenderer sr;
    private Color old;
    public GameObject kz;

    void Awake(){
        sr = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        old = sr.color;
        kz = GameObject.Find("KillZone");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(key)){
            StartCoroutine(Pressed());
        }
        if(Input.GetKeyDown(key) && active){
            Destroy(note);
            kz.GetComponent<KillZone>().AddStreak();
            AddScore();
        }
        else if(Input.GetKeyDown(key) && !active){
            kz.GetComponent<KillZone>().ResetStreak();
        }
    }

    void OnTriggerEnter2D(Collider2D col){
        active = true;
        if(col.gameObject.tag == "Note"){
            note = col.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D col){
        active = false;
    }

    void AddScore(){
        PlayerPrefs.SetInt("Score", PlayerPrefs.GetInt("Score")+kz.GetComponent<KillZone>().GetScore());
    }

    IEnumerator Pressed(){
        sr.color = new Color(0,0,0);
        yield return new WaitForSeconds(0.05f);
        sr.color = old;
    }
}
