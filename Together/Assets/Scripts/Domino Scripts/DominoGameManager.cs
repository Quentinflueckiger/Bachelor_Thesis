using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class DominoGameManager : MonoBehaviour
{
    [HideInInspector]
    public NetworkLobbyManager lobbyManager;

    public int nbrOfDominosAtStart;
    public GameObject dominoPrefab;
    public GameObject controllerPanel;

    public Text turnText;

    public List<DominoCard> dominoCards = new List<DominoCard>();
    private static List<DominoCard> dominoAvailable;

    private List<GameObject> players = new List<GameObject>();
    private bool endGame = false;
    private bool gameStart = false;

    private int playerTurn = 0;

    public static DominoGameManager Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);

        this.lobbyManager = FindObjectOfType<NetworkLobbyManager>();
    }
    
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (players.Count > 0 && !gameStart)
        {
            if (!endGame)
                StartGame();
        }
    }

    private void StartGame()
    {
        gameStart = true;
        endGame = false;
        dominoAvailable = new List<DominoCard>(dominoCards);

        SetPlayersHand();

        players[playerTurn].GetComponent<DominoPlayer>().SetTurn(true);
        SetTurnText(players[playerTurn].GetComponent<DominoSetUpPlayer>().GetPlayerName());
    }

    public void AddPlayer(GameObject player)
    {
        players.Add(player);
        Debug.Log("Player added, number of players : " + players.Count);
    }

    public void RemovePlayer(GameObject player)
    {
        players.Remove(player);
        Debug.Log("Removed 1 player");
    }

    public void NextPlayer()
    {
        players[playerTurn].GetComponent<DominoPlayer>().SetTurn(false);
        IncrementPlayerTurn();
        players[playerTurn].GetComponent<DominoPlayer>().SetTurn(true);
        SetTurnText(players[playerTurn].GetComponent<DominoSetUpPlayer>().GetPlayerName());
    }

    private void IncrementPlayerTurn()
    {
        if (playerTurn < players.Count)
        {
            playerTurn++;
        }
        else
        {
            playerTurn = 0;
        }
        
    }

    private void SetTurnText(string text)
    {
        turnText.text = text + "'s turn.";
    }

    private void SetPlayersHand()
    {
        foreach (GameObject player in players)
        {
            player.GetComponent<DominoPlayer>().SetHand();
        }
    }

    #region Dominos logic
    public int GetNumberOfDominos()
    {
        return nbrOfDominosAtStart;
    }

    public DominoCard DrawDomino()
    {
        if (dominoAvailable.Count < 2)
            return null;
        int random = UnityEngine.Random.Range(0, dominoAvailable.Count - 1);
        DominoCard dominoToReturn = dominoAvailable[random];
        dominoAvailable.RemoveAt(random);
        Debug.Log("domino list : " + dominoAvailable.Count);

        return dominoToReturn;
    }
    #endregion


    private void TestSpawn()
    {
        int x = 6;
        Debug.Log("domino list : " + dominoAvailable.Count);
        for (int i = 0; i < x; i++)
        {
            GameObject dominoToInstantiate = Instantiate(dominoPrefab, new Vector3(), Quaternion.identity);
            int random = UnityEngine.Random.Range(0, dominoAvailable.Count - 1);
            dominoToInstantiate.GetComponent<DominoDisplay>().SetDominoCard(dominoAvailable[random]);
            dominoToInstantiate.transform.parent = controllerPanel.transform;
            dominoAvailable.RemoveAt(random);
        }
        Debug.Log("domino list : " + dominoAvailable.Count);

    }
}
