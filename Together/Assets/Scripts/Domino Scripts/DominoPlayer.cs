using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DominoPlayer : NetworkBehaviour
{
    public static int playerNbr = 0;

    private bool isMyTurn = false;
    private List<DominoCard> hand = new List<DominoCard>();

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
        if (isMyTurn)
        {
            // Allow to play
        }
        else
        {
            // Deny ability to play
        }
    }

    public void SetTurn(bool turn)
    {
        isMyTurn = turn;
    }

    public void OnDestroy()
    {
        dgm.RemovePlayer(this.gameObject);
        playerNbr--;
    }
}
