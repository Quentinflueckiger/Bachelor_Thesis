using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Prototype.NetworkLobby;

public class DominoUIHandler : MonoBehaviour
{
    private GameObject turnText;
    private GameObject lobbyTopPanel;
    private GameObject lobbyMainPanel;
    private NetworkLobbyManager lobbyManager;

    void Start()
    {
        turnText = GameObject.Find("TurnText");
        lobbyTopPanel = GameObject.Find("TopPanel");
        lobbyMainPanel = GameObject.Find("LobbyPanel");
        lobbyManager = FindObjectOfType<NetworkLobbyManager>();

        lobbyTopPanel.SetActive(false);
        lobbyMainPanel.SetActive(false);

        // Checks plateform before runtime
        // If it's a standalone version or in the editor it means it is the server
#if UNITY_EDITOR || UNITY_STANDALONE
        turnText.SetActive(true);
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
