using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyScene : MonoBehaviour
{
    public static LobbyScene Instance { private set; get; }

    void Start()
    {
        Instance = this;
    }

    #region Tests
    
    #endregion
}
