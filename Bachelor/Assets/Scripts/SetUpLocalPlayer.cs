using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SetUpLocalPlayer : NetworkBehaviour
{
    PlayerController playerController;
    private void Start()
    {
        playerController = GetComponent<PlayerController>();
        if (isLocalPlayer)
        {
            playerController.enabled = true;
        }
        else
        {
            playerController.enabled = false;
        }
    }

    void OnGUI()
    {
        if (isLocalPlayer)
        {
            if (GUI.Button(new Rect(500, 300, 45, 25), "Up"))
                playerController.GoUp(); ;
            if (GUI.Button(new Rect(475, 325, 45, 25), "Left"))
                playerController.GoLeft();
            if (GUI.Button(new Rect(525, 325, 45, 25), "Right"))
                playerController.GoRight();
            if (GUI.Button(new Rect(500, 350, 45, 25), "Down"))
                playerController.GoDown();
        }
    }
}
