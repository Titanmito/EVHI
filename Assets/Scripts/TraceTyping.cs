using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TraceTyping : MonoBehaviour
{
    public InputField inputField = null;
    private float typingTime;
    private uint traceCount = (uint)0;
    // Start is called before the first frame update
    void Start()
    {
        typingTime = 0.0f;
        traceCount = (uint)0;
        if (inputField != null)
            inputField.onEndEdit.AddListener(delegate { OnEndEdit(); });
    }

    // Update is called once per frame
    void Update()
    {
        if (inputField != null)
            if (inputField.isFocused)
                typingTime += Time.deltaTime;
    }

    public void OnEndEdit()
    {
        typingTime = typingTime / inputField.text.Length;
        GameObject.Find(GlobalVariables.goNameUsersManager).GetComponent<ManageUsers>().currentUserTypingTime =
            (GameObject.Find(GlobalVariables.goNameUsersManager).GetComponent<ManageUsers>().currentUserTypingTime *
            ((int)traceCount) + typingTime) / ((int)traceCount + 1);
        // Debug.Log("Last typing time: " + typingTime.ToString());
        // Debug.Log("Interm mean: " + GameObject.Find(GlobalVariables.goNameUsersManager).GetComponent<ManageUsers>().currentUserTypingTime.ToString());
        traceCount++;
        typingTime = 0.0f;
    }
}
