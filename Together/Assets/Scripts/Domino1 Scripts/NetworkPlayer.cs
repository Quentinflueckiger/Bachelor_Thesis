﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class NetworkPlayer : NetworkBehaviour
{

    [SyncVar(hook = "OnTurnChange")]
    public bool isTurn = false;

    [SyncVar(hook = "UpdateTimeDisplay")]
    public float time = 100;

    [SyncVar(hook = "UpdateTurnDisplay")]
    public string turnText = "";

    public PlayerControllerigno controller;
    public GameObject controllerPanel;

    [SyncVar]
    public bool ready = false;

    // Use this for initialization
    void Start()
    {
        controller.OnPlayerInput += OnPlayerInput;
        if (isLocalPlayer)
            controllerPanel.SetActive(true);
    }

    // Update is called once per frame
    [Server]
    void Update()
    {
        if (isTurn)
        {
            time -= Time.deltaTime;
            if (time <= 0)
            {
                CustomManager.Instance.AlterTurns();
            }
        }
    }

    [Server]
    public void EndTurn()
    {
        CustomManager.Instance.AlterTurns();
    }

    public override void OnStartClient()
    {
        DontDestroyOnLoad(this);

        base.OnStartClient();
        Debug.Log("Client Network Player start");
        StartPlayer();

        CustomManager.Instance.RegisterNetworkPlayer(this);
    }

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
        controller.SetupLocalPlayer();
    }

    [Server]
    public void StartPlayer()
    {
        ready = true;
    }

    public void StartGame()
    {
        TurnStart();
    }

    [Server]
    public void TurnStart()
    {
        Debug.Log("Turn start for player : " + this.netId);
        isTurn = true;
        time = 15;
        //turnText = "Player " + this.netId +"'s turn.";
        RpcTurnStart();
    }

    [ClientRpc]
    void RpcTurnStart()
    {
        controller.TurnStart();
    }

    [Server]
    public void TurnEnd()
    {
        isTurn = false;
        RpcTurnEnd();
    }

    [ClientRpc]
    void RpcTurnEnd()
    {
        controller.TurnEnd();
    }

    public override void OnNetworkDestroy()
    {
        base.OnNetworkDestroy();
        CustomManager.Instance.DeregisterNetworkPlayer(this);
    }

    public void OnTurnChange(bool turn)
    {
        if (isLocalPlayer)
        {
            //play turn sound 
        }
    }

    public void UpdateScore(int score)
    {
        Debug.Log("score:" + score);
    }

    void OnPlayerInput(PlayerAction action, float amount)
    {
        if (action == PlayerAction.SHOOT)
        {
            CmdOnPlayerInput(action, amount);
        }
    }

    [Command]
    void CmdOnPlayerInput(PlayerAction action, float amount)
    {
        //Shoot bullets

        //Update score
        CustomManager.Instance.UpdateScore((int)amount);
    }

    public void UpdateTurnDisplay(string turn)
    {
        GameObject turnTextGO = GameObject.Find("TurnText");
        Text turnText = turnTextGO.GetComponent<Text>();
        turnText.text = turn;
    }

    public void UpdateTimeDisplay(float curtime)
    {
        GameObject timerText = GameObject.FindWithTag("Timer");
        Text timer = timerText.GetComponent<Text>();
        timer.text = Mathf.Round(curtime).ToString();
    }

    public void SetTurn()
    {
        isTurn = true;
    }
}