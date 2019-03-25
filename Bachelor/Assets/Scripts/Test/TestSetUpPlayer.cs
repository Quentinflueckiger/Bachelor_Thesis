using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TestSetUpPlayer : NetworkBehaviour
{

    private TestPlayerController tpc;

    void Start()
    {
        tpc = GetComponent<TestPlayerController>();

        if (isLocalPlayer)
        {
            tpc.enabled = true;
        }
        else
        {
            tpc.enabled = false;
        }
    }

    /*
    void OnGUI()
    {
        if (isLocalPlayer)
        {
            if (GUI.Button(new Rect(500, 300, 45, 25), "Up"))
                tpc.GoUp(); ;
            if (GUI.Button(new Rect(475, 325, 45, 25), "Left"))
                tpc.GoLeft();
            if (GUI.Button(new Rect(525, 325, 45, 25), "Right"))
                tpc.GoRight();
            if (GUI.Button(new Rect(500, 350, 45, 25), "Down"))
                tpc.GoDown();
        }
    }
    */
}
