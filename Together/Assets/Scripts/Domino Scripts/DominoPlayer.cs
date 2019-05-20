using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DominoPlayer : NetworkBehaviour
{
    public static int playerNbr = 0;
    public GameObject dominoPrefab;
    public GameObject canvasController;

    public Transform start;
    public float dominoSpacing;

    private float nbrOfDominos;
    private int nbrOfStartDominos;
    private bool isMyTurn = false;
    public List<DominoCard> hand = new List<DominoCard>();

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
            nbrOfStartDominos = dgm.GetNumberOfDominos();
        }

        nbrOfDominos = 0.0f;
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

    public void SetHand(int[] handIndex)
    {
        foreach (var dominoIndex in handIndex)
        {
            hand.Add(dgm.GetDominoAt(dominoIndex));
        }
        //this.hand = new List<DominoCard>(handIndex);
        /*for (int i = 0; i < nbrOfStartDominos; i++)
        {
            DominoCard domino = dgm.DrawDomino();
            hand.Add(domino);
            UpdateHand(domino);
        }*/
        foreach (var domino in this.hand)
        {
            UpdateHand(domino);
        }
    }

    [Command]
    public void CmdDrawDomino()
    {
        DominoCard domino = dgm.GetDominoAt(dgm.DrawDominoIndex());
        hand.Add(domino);
        UpdateHand(domino);
        RpcDrawDomino();
    }

    [ClientRpc]
    public void RpcDrawDomino()
    {
        DominoCard domino = dgm.GetDominoAt(dgm.DrawDominoIndex());
        hand.Add(domino);
        UpdateHand(domino);
    }

    private void UpdateHand(DominoCard domino)
    {
        GameObject dominoToInstantiate = Instantiate(dominoPrefab, new Vector3(0,0,0), Quaternion.identity);
        dominoToInstantiate.GetComponent<DominoDisplay>().SetDominoCard(domino);
        dominoToInstantiate.transform.parent = canvasController.transform;
        dominoToInstantiate.transform.localScale.Set(.5f, .5f, 1.0f);
        dominoToInstantiate.transform.position = start.position + new Vector3((nbrOfDominos * dominoSpacing), 0, 0);
        nbrOfDominos++;
    }
    
    public void OnDestroy()
    {
        dgm.RemovePlayer(this.gameObject);
        playerNbr--;
    }
}
