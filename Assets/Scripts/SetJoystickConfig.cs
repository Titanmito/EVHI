using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class SetJoystickConfig : MonoBehaviour
{
    public Dropdown dropdown;
    public Button button;
    public void Apply()
    {
        // Debug.Log(dropdown.value);
        if (dropdown.value == 0)
        {
            GameObject.Find(GlobalVariables.goNameJoystickManager).GetComponent<ManageJoystickConfig>().config = GlobalVariables.dualshock4Buttons;
            GameObject.Find(GlobalVariables.goNameJoystickManager).GetComponent<ManageJoystickConfig>().configName = GlobalVariables.dualshock4Name;
            GameObject.Find(GlobalVariables.goNameJoystickManager).GetComponent<ManageJoystickConfig>().model = GlobalVariables.JoystickModel.Dualshock4;
        }
        else if (dropdown.value == 1)
        {
            GameObject.Find(GlobalVariables.goNameJoystickManager).GetComponent<ManageJoystickConfig>().config = GlobalVariables.thrustmasterESwapProContollerButtons;
            GameObject.Find(GlobalVariables.goNameJoystickManager).GetComponent<ManageJoystickConfig>().configName = GlobalVariables.thrustmasterESwapProContollerName;
            GameObject.Find(GlobalVariables.goNameJoystickManager).GetComponent<ManageJoystickConfig>().model = GlobalVariables.JoystickModel.ThrustmasterESwapProContoller;
        }
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync(gameObject.scene);
    }

    public void Start()
    {
        if (GameObject.Find(GlobalVariables.goNameJoystickManager).GetComponent<ManageJoystickConfig>().model == GlobalVariables.JoystickModel.Dualshock4)
            dropdown.value = 0;
        else if (GameObject.Find(GlobalVariables.goNameJoystickManager).GetComponent<ManageJoystickConfig>().model == GlobalVariables.JoystickModel.ThrustmasterESwapProContoller)
            dropdown.value = 1;
        button.interactable = false;
    }
}
