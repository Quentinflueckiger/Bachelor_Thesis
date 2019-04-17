using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Prototype.NetworkLobby;

public class SnakeLobbyHook : LobbyHook
{
    public override void OnLobbyServerSceneLoadedForPlayer(NetworkManager manager, GameObject lobbyPlayer, GameObject gamePlayer)
    {
        LobbyPlayer lobby = lobbyPlayer.GetComponent<LobbyPlayer>();
        SnakeSetUpPlayer snake = gamePlayer.GetComponent<SnakeSetUpPlayer>();

        snake.playerName = lobby.playerName;
        snake.playerColour = lobby.playerColor;
    }
}
