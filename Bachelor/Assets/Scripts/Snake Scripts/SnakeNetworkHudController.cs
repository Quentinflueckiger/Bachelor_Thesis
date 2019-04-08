using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

[RequireComponent(typeof(NetworkManager))]
public class SnakeNetworkHudController : MonoBehaviour
{
    [HideInInspector]
    public NetworkManager manager;

    // Used as singleton
    public static SnakeNetworkHudController Instance;

    void Awake()
    {
        Instance = this;
        this.manager = this.GetComponent<NetworkManager>();
    }

    public void ConnectTo(Text textIP)
    {
        this.manager.StartClient();
        this.manager.networkAddress = textIP.text;
    }

    public void StartServer()
    {
        this.manager.StartServer();
    }

    public void StopServer()
    {
        if (this.IsServerStarted())
            this.manager.StopHost();
        else
            Debug.Log("Server not started");
    }

    public bool IsServerStarted()
    {
        return NetworkServer.active;
    }
}
