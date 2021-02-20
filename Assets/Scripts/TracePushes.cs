using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TracePushes : MonoBehaviour
{
    private List<GlobalVariables.JoystickButton> PushedButtons;
    private List<float> PushedMoments;
    private Dictionary<GlobalVariables.JoystickButton, KeyCode> config;
    private float timeCounter;
    // Start is called before the first frame update
    void Start()
    {
        timeCounter = 0.0f;
        PushedButtons = GameObject.Find(GlobalVariables.goNameButtonPushContainer).GetComponent<ButtonPushContainer>().PushedButtons;
        PushedButtons.Clear();
        PushedMoments = GameObject.Find(GlobalVariables.goNameButtonPushContainer).GetComponent<ButtonPushContainer>().PushedMoments;
        PushedMoments.Clear();
        config = GameObject.Find(GlobalVariables.goNameJoystickManager).GetComponent<ManageJoystickConfig>().config;
    }

    // Update is called once per frame
    void Update()
    {
        timeCounter += Time.deltaTime;
        foreach (KeyCode key in GlobalVariables.JoystickKeys)
        {
            if (Input.GetKeyDown(key))
            {
                PushedButtons.Add(GlobalVariables.DictionaryUtiles<GlobalVariables.JoystickButton, KeyCode>.GetKey(config, key));
                PushedMoments.Add(timeCounter);
            }
        }
    }
}
