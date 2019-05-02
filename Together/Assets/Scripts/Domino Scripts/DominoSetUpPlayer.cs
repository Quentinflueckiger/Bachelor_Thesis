using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class DominoSetUpPlayer : NetworkBehaviour
{
    [SyncVar(hook = "OnChangeName"), HideInInspector]
    public string playerName = "player name";

    public GameObject controller;
    public GameObject canvasController;

    private DominoPlayer dp;
    private DominoGameManager dgm;

    void Start()
    {
        dgm = DominoGameManager.Instance;

        if (dgm == null)
            Debug.Log("Failed to retrieve game manager.");

        dp = GetComponent<DominoPlayer>();

        // Enable the player script if it is the local player
        if (isLocalPlayer)
        {
            dp.enabled = true;
            controller.SetActive(true);
            canvasController.SetActive(true);
        }
        else
        {
            dp.enabled = false;
            canvasController.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnChangeName(string n)
    {
        playerName = n;
    }

    public string GetPlayerName()
    {
        return playerName;
    }
}
