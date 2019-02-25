using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;
using UnityEngine;

public class NetworkClientUI : MonoBehaviour {

    NetworkClient client;

    private void OnGUI()
    {
        string ipAdress = LocalIPAddress();
        GUI.Box(new Rect(10, Screen.height - 50, 100, 50), ipAdress);
        GUI.Label(new Rect(20, Screen.height - 30, 100, 20), "Status: " + client.isConnected);

        if(!client.isConnected)
        {
            if (GUI.Button(new Rect(10, 10, 60, 50), "Connect"))
                //Connect();
                ;
        }
    }

    // Use this for initialization
    void Start () {
        client = new NetworkClient();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void Connect()
    {
        client.Connect("147.87.19.146", 25000);
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
