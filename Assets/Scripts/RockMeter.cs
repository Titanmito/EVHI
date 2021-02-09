using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockMeter : MonoBehaviour
{

    float rm;
    GameObject needle;
    // Start is called before the first frame update
    void Start()
    {
        needle = transform.Find("needle").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        rm = PlayerPrefs.GetInt("RockMetter");
        rm = (rm-25)*12;
        needle.transform.localPosition = new Vector3(rm/150, 0, 0);
    }
}
