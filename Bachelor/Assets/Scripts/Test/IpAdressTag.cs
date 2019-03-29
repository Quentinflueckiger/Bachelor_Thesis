using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IpAdressTag : MonoBehaviour
{

    public Text ipText;
    private string ip;

    void Start()
    {
        ip = ShowIp.Instance.LocalIPAddress();

        ipText.text = ip;
    }

}
