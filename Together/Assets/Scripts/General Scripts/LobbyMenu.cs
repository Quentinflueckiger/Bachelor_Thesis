using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Prototype.NetworkLobby;
using System.Net;

//Main menu, mainly only a bunch of callback called by the UI (setup throught the Inspector)
public class LobbyMenu : MonoBehaviour
{
    public LobbyManager lobbyManager;

    //public RectTransform lobbyServerList;
    public RectTransform lobbyPanel;

    public InputField ipInput;

    public GameObject mainPanel;

    private string ip;

    public void OnEnable()
    {
        lobbyManager.topPanel.ToggleVisibility(true);

        ipInput.onEndEdit.RemoveAllListeners();
        ipInput.onEndEdit.AddListener(onEndEditIP);

        ip = LocalIPAddress();

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
        lobbyManager.networkAddress = ip;
        lobbyManager.StartServer();

        lobbyManager.backDelegate = lobbyManager.StopServerClbk;

        lobbyManager.SetServerInfo("Dedicated Server", lobbyManager.networkAddress);

        mainPanel.SetActive(false);
    }

    void onEndEditIP(string text)
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            OnClickJoin();
        }
    }

    private string LocalIPAddress()
    {
        IPHostEntry host;
        string localIP = "";
        host = Dns.GetHostEntry(Dns.GetHostName());
        foreach (IPAddress ip in host.AddressList)
        {
            if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
            {
                localIP = ip.ToString();
                break;
            }
        }
        return localIP;
    }

}
