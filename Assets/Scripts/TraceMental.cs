using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraceMental : MonoBehaviour
{
    private User currentUser = null;
    private Vector3 mousePosition;
    private ushort traceCount = 1, traceIndex = 0;
    private float eps = 0.001f;
    private float mentalTime;
    // Start is called before the first frame update
    void Start()
    {
        mousePosition = Input.mousePosition;
        currentUser = GameObject.Find(GlobalVariables.goNameUsersManager).GetComponent<ManageUsers>().currentUser;
        mentalTime = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        mentalTime += Time.deltaTime;
        if (currentUser != null && traceIndex < traceCount)
        {
            float mouseDeltaAbs = Vector3.Distance(Input.mousePosition, mousePosition);
            if (mouseDeltaAbs > eps)
            {
                // Debug.Log("I do a trace for user " + currentUser.Name + " with mental time: " + mentalTime.ToString());
                GameObject.Find(GlobalVariables.goNameUsersManager).GetComponent<ManageUsers>().DumpMentalPointingTyping(mental: mentalTime);
                traceIndex += 1;
            }
        }
    }
}
