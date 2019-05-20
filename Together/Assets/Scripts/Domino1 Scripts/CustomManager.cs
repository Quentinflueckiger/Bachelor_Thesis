using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Networking;

public class CustomManager : NetworkManager
{

    public static CustomManager Instance;
    List<NetworkPlayer> players;

    private bool gameStarted = false;
    int iActivePlayer = 0;
    public int ActivePlayer
    {
        get
        {
            return iActivePlayer;
        }
    }

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        players = new List<NetworkPlayer>();
    }

    private void Update()
    {
        if (!gameStarted)
        {
            if (players.Count > 1)
            {
                CheckPlayersReady();
            }
        }
        
    }

    bool CheckPlayersReady()
    {
        bool playersReady = true;
        foreach (var player in players)
        {
            playersReady &= player.ready;
        }

        if (playersReady)
        {
            players[iActivePlayer].StartGame();
            gameStarted = true;
        }

        return playersReady;
    }

    public void ReTurn()
    {
        Debug.Log("turn::" + iActivePlayer);
        players[iActivePlayer].TurnStart();
    }

    public void AlterTurns()
    {
        Debug.Log("turn::" + iActivePlayer);

        players[iActivePlayer].TurnEnd();
        iActivePlayer = (iActivePlayer + 1) % players.Count;
        players[iActivePlayer].turnText = "Player " + (iActivePlayer+1) + "'s turn.";
        players[iActivePlayer].TurnStart();
    }

    public void UpdateScore(int score)
    {
        players[ActivePlayer].UpdateScore(score);
    }

    public void RegisterNetworkPlayer(NetworkPlayer player)
    {
        if (players.Count <= 2)
        {
            players.Add(player);
        }
    }

    public void DeregisterNetworkPlayer(NetworkPlayer player)
    {
        players.Remove(player);
    }
    

}