using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using UnityEngine.Networking;

public class ShowIp : MonoBehaviour
{

    #region Singleton
    public static ShowIp Instance;

    private void Awake()
    {
        Instance = this;
    }
    #endregion

    private void Start()
    {
        string ipAdress = LocalIPAddress();
        Debug.Log("Ip adress :" + ipAdress);
    }

    private void OnGui()
    {
        string ipAdress = LocalIPAddress();
        GUI.Box(new Rect(10, Screen.height - 50, 100, 50), ipAdress);
        GUI.Label(new Rect(20, Screen.height - 35, 100, 20), "Status: " + NetworkServer.active);
        GUI.Label(new Rect(20, Screen.height - 20, 100, 20), "Connected: " + NetworkServer.connections.Count);
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
