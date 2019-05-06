using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyUISetUp : MonoBehaviour
{

    public GameObject serverPanel;
    public GameObject clientPanel;

    // Start is called before the first frame update
    void Start()
    {
#if UNITY_EDITOR //|| UNITY_STANDALONE
        serverPanel.SetActive(true);
        clientPanel.SetActive(false);
#else
        serverPanel.SetActive(false);
        clientPanel.SetActive(true);
#endif
    }
}
