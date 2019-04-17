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
