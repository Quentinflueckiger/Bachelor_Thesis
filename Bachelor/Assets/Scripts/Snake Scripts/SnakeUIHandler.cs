using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SnakeUIHandler : MonoBehaviour
{
    private GameObject connectPanel;
    private GameObject startServerPanel;
    private GameObject startServerBtn;
    private GameObject stopServerBtn;
    private TestNetworkHudController tnhc;

    void Start()
    {
        tnhc = TestNetworkHudController.Instance;
        connectPanel = GameObject.Find("ConnectPanel");
        startServerPanel = GameObject.Find("ServerPanel");
        startServerBtn = GameObject.Find("StartServerBtn");
        stopServerBtn = GameObject.Find("StopServerBtn");

        // Checks plateform before runtime
        // If it's a standalone version or in the editor it means it is the server
        // UNITY_STANDALONE ||
#if UNITY_EDITOR
        connectPanel.SetActive(false);
        startServerPanel.SetActive(true);
        stopServerBtn.SetActive(false);

        // Otherwise it is the client
#else
        connectPanel.SetActive(true);
        startServerPanel.SetActive(false);
#endif
    }

    // TODO : Check if connection succeed
    public void OnConnectBtnPressed(Text ip)
    {
        tnhc.ConnectTo(ip);
    }

    public void OnStartServerPressed()
    {
        tnhc.StartServer();
        if (tnhc.IsServerStarted())
        {
            startServerBtn.SetActive(false);
            stopServerBtn.SetActive(true);
        }
        else
            Debug.LogError("Couldn't start the server.");
    }

    public void OnStopServerPressed()
    {
        tnhc.StopServer();
        if (!tnhc.IsServerStarted())
        {
            stopServerBtn.SetActive(false);
            startServerBtn.SetActive(true);
        }
        else
            Debug.LogError("Couldn't stop the server.");
    }
}
