using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MakeButtonInteractable : MonoBehaviour
{
    public Button button;
    public void Make()
    {
        button.interactable = true;
    }
}
