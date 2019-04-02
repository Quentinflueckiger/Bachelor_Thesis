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
#if UNITY_EDITOR
        ip = ShowIp.Instance.LocalIPAddress();

        ipText.text = ip;
#else
        ipText.gameObject.SetActive(false);
#endif
    }

}
