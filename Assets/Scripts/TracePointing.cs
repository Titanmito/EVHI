using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TracePointing : MonoBehaviour
{
    private User currentUser = null;
    private Vector3 mousePosition;
    // private ushort traceCount = 1, traceIndex = 0;
    private float eps = 0.001f;
    private float pointingTime;
    private bool tracing = false;
    private ushort framesBeforeStopTracingLimit = (ushort)30, framesBeforeStopTracingProgress = (ushort)0;
    // Start is called before the first frame update
    void Start()
    {
        mousePosition = Input.mousePosition;
        currentUser = GameObject.Find(GlobalVariables.goNameUsersManager).GetComponent<ManageUsers>().currentUser;
        pointingTime = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentUser != null)
        {
            if (tracing)
            {
                pointingTime += Time.deltaTime;
                float mouseDeltaAbs = Vector3.Distance(Input.mousePosition, mousePosition);
                if (mouseDeltaAbs < eps)
                    framesBeforeStopTracingProgress++;
                else
                {
                    framesBeforeStopTracingProgress = (ushort)0;
                    mousePosition = Input.mousePosition;
                }
                if (framesBeforeStopTracingProgress >= framesBeforeStopTracingLimit)
                {
                    tracing = false;
                    pointingTime -= ((int)framesBeforeStopTracingLimit) * Time.deltaTime;
                    // Debug.Log("I do a trace for user " + currentUser.Name + " with pointing time: " + pointingTime.ToString());
                    GameObject.Find(GlobalVariables.goNameUsersManager).GetComponent<ManageUsers>().DumpMentalPointingTyping(pointing: pointingTime);
                }
            }
            else
            {
                float mouseDeltaAbs = Vector3.Distance(Input.mousePosition, mousePosition);
                if (mouseDeltaAbs > eps)
                {
                    tracing = true;
                    pointingTime = 0.0f;
                    mousePosition = Input.mousePosition;
                    framesBeforeStopTracingProgress = (ushort)0;
                }
            }
        }
    }
}
