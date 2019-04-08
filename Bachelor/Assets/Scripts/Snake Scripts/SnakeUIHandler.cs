using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class SnakeUIHandler : MonoBehaviour
{
    private GameObject connectPanel;
    private GameObject startServerPanel;
    private GameObject startServerBtn;
    private GameObject stopServerBtn;
    private GameObject timerText;
    private SnakeNetworkHudController snhc;

    void Start()
    {
        snhc = SnakeNetworkHudController.Instance;
        connectPanel = GameObject.Find("ConnectPanel");
        startServerPanel = GameObject.Find("ServerPanel");
        startServerBtn = GameObject.Find("StartServerBtn");
        stopServerBtn = GameObject.Find("StopServerBtn");
        timerText = GameObject.Find("GameTimer");

        NetworkManagerHUD hud = FindObjectOfType<NetworkManagerHUD>();

        // Checks plateform before runtime
        // If it's a standalone version or in the editor it means it is the server
        // TODO : UNITY_STANDALONE ||
#if UNITY_EDITOR || UNITY_STANDALONE
        connectPanel.SetActive(false);
        startServerPanel.SetActive(true);
        stopServerBtn.SetActive(false);
        timerText.SetActive(true);

        // Otherwise it is the client
#else
        connectPanel.SetActive(true);
        startServerPanel.SetActive(false);
        timerText.SetActive(false);
#endif

        if (hud != null)
            hud.showGUI = false;
    }

    // TODO : Check if connection succeed
    public void OnConnectBtnPressed(Text ip)
    {
        snhc.ConnectTo(ip);
    }

    public void OnStartServerPressed()
    {
        snhc.StartServer();
        if (snhc.IsServerStarted())
        {
            startServerBtn.SetActive(false);
            stopServerBtn.SetActive(true);
        }
        else
            Debug.LogError("Couldn't start the server.");
    }

    public void OnStopServerPressed()
    {
        snhc.StopServer();
        if (!snhc.IsServerStarted())
        {
            stopServerBtn.SetActive(false);
            startServerBtn.SetActive(true);
        }
        else
            Debug.LogError("Couldn't stop the server.");
    }
}
