using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using Prototype.NetworkLobby;

public class SnakeUIHandler : MonoBehaviour
{
    private GameObject timerText;
    private GameObject lobbyTopPanel;
    private NetworkLobbyManager lobbyManager;

    void Start()
    {
        timerText = GameObject.Find("GameTimer");
        lobbyTopPanel = GameObject.Find("TopPanel");
        lobbyManager = FindObjectOfType<NetworkLobbyManager>();

        NetworkManagerHUD hud = FindObjectOfType<NetworkManagerHUD>();

        lobbyTopPanel.SetActive(false);

        // Checks plateform before runtime
        // If it's a standalone version or in the editor it means it is the server
#if UNITY_EDITOR || UNITY_STANDALONE
        timerText.SetActive(true);
        if (hud != null)
            hud.showGUI = false;
        // Otherwise it is the client
#else
        timerText.SetActive(false);
#endif
    } 

    public void BackBtn()
    {
        lobbyManager.GetComponent<LobbyManager>().GoBackButton();

    }
}
