using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Prototype.NetworkLobby;

public class DominoLobbyHook : LobbyHook
{
    public override void OnLobbyServerSceneLoadedForPlayer(NetworkManager manager, GameObject lobbyPlayer, GameObject gamePlayer)
    {
        LobbyPlayer lobby = lobbyPlayer.GetComponent<LobbyPlayer>();
        DominoSetUpPlayer domino = gamePlayer.GetComponent<DominoSetUpPlayer>();

        if (domino != null)
            domino.playerName = lobby.playerName;
        else
        {
            NetworkPlayer dominoPlayer = gamePlayer.GetComponent<NetworkPlayer>();
            if (dominoPlayer != null)
                dominoPlayer.playerName = lobby.playerName;
        }
    }
}
