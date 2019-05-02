using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DominoPlayer : NetworkBehaviour
{
    public static int playerNbr = 0;

    DominoGameManager dgm;

    private void Awake()
    {

        dgm = DominoGameManager.Instance;
        playerNbr++;
    }

    void Start()
    {
        if (dgm != null)
        {
            dgm.AddPlayer(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnDestroy()
    {
        dgm.RemovePlayer(this.gameObject);
    }
}
