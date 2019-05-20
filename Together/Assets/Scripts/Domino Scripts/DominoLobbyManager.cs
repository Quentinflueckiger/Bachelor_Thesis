using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Prototype.NetworkLobby;

public class DominoLobbyManager : LobbyManager
{
    public static DominoLobbyManager Instance;

    List<NetworkPlayer> players;
    private bool gameScene = false;

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

        players = new List<NetworkPlayer>();
    }

    private void Update()
    {
        if (gameScene)
        {

            if (players.Count > 1)
            {
                Debug.Log("Check");
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

    public override bool OnLobbyServerSceneLoadedForPlayer(GameObject lobbyPlayer, GameObject gamePlayer)
    {
        Debug.Log("OnLobbyServerSceneLoadedForPlayer");
        gameScene = true;
        return base.OnLobbyServerSceneLoadedForPlayer(lobbyPlayer, gamePlayer);
    }
    public override void ServerChangeScene(string sceneName)
    {
        base.ServerChangeScene(sceneName);
        gameScene = true;
    }
}
/*
 * 
    public static DominoLobbyManager Instance;

    List<NetworkPlayer> players;
    private bool gameScene = false;

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
        if (gameScene)
        {
            if (players.Count > 1)
            {
                Debug.Log("Check");
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
 */
