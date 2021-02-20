using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPushContainer : MonoBehaviour
{
    [HideInInspector]
    public List<GlobalVariables.JoystickButton> RealButtons, PushedButtons;
    [HideInInspector]
    public List<float> RealMoments, PushedMoments;
    [HideInInspector]
    public ushort SuccessfulPushes;
    // Start is called before the first frame update
    void Start()
    {
        RealButtons = new List<GlobalVariables.JoystickButton>();
        PushedButtons = new List<GlobalVariables.JoystickButton>();
        RealMoments = new List<float>();
        PushedMoments = new List<float>();
    }

    void Update()
    {
        //string s = "";
        //if (PushedButtons.Count > 0)
        //{
        //    foreach (GlobalVariables.JoystickButton btn in PushedButtons)
        //        s += btn.ToString() + " ";
        //    Debug.Log(s);
        //    Debug.Log(PushedButtons.Count);
        //}
    }
}
