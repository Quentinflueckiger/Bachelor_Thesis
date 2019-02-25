using System.Collections;
using System.Collections.Generic;
using System;
using System.Net;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;
using UnityEngine;

public class NetworkServerUI : MonoBehaviour {

    private void OnGUI()
    {
        string ipAdress = LocalIPAddress();
        GUI.Box(new Rect(10, Screen.height - 50, 100, 50), ipAdress);
        GUI.Label(new Rect(20, Screen.height - 35, 100, 20), "Status: " + NetworkServer.active);
        GUI.Label(new Rect(20, Screen.height - 20, 100, 20), "Connected: " + NetworkServer.connections.Count);
    }

    // Use this for initialization
    void Start () {
        NetworkServer.Listen(25000);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public string LocalIPAddress()
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
