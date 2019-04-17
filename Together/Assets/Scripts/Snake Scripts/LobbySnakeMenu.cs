using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Prototype.NetworkLobby;

//Main menu, mainly only a bunch of callback called by the UI (setup throught the Inspector)
public class LobbySnakeMenu : MonoBehaviour
{
    public LobbyManager lobbyManager;

    public RectTransform lobbyServerList;
    public RectTransform lobbyPanel;

    public InputField ipInput;

    public GameObject mainPanel;

    private string ip;

    public void OnEnable()
    {
        lobbyManager.topPanel.ToggleVisibility(true);

        ipInput.onEndEdit.RemoveAllListeners();
        ipInput.onEndEdit.AddListener(onEndEditIP);

        ip = ShowIp.Instance.LocalIPAddress();

    }

    public void OnClickJoin()
    {
        lobbyManager.ChangeTo(lobbyPanel);

        lobbyManager.networkAddress = ipInput.text;
        lobbyManager.StartClient();

        lobbyManager.backDelegate = lobbyManager.StopClientClbk;
        lobbyManager.DisplayIsConnecting();

        lobbyManager.SetServerInfo("Connecting...", lobbyManager.networkAddress);

        mainPanel.SetActive(false);
    }

    public void OnClickDedicated()
    {
        lobbyManager.ChangeTo(null);
        lobbyManager.StartServer();

        lobbyManager.backDelegate = lobbyManager.StopServerClbk;

        lobbyManager.SetServerInfo("Dedicated Server", ip);// lobbyManager.networkAddress);

        mainPanel.SetActive(false);
    }

    void onEndEditIP(string text)
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            OnClickJoin();
        }
    }

}
