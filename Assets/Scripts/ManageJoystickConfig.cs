using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageJoystickConfig : MonoBehaviour
{
    [HideInInspector]
    public Dictionary<GlobalVariables.JoystickButton, KeyCode> config;
    [HideInInspector]
    public string configName = GlobalVariables.dualshock4Name;
    [HideInInspector]
    public GlobalVariables.JoystickModel model = GlobalVariables.JoystickModel.Dualshock4;
    void Start()
    {
        config = new Dictionary<GlobalVariables.JoystickButton, KeyCode>(GlobalVariables.dualshock4Buttons);
    }
}
