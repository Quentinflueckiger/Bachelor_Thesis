using System.Collections;
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

    [SyncVar(hook = "OnChangeName"), HideInInspector]
    public string playerName = "player name";

    public DominoPTest controller;
    public GameObject controllerPanel;

    [SyncVar]
    public bool ready = false;

    // Use this for initialization
    void Start()
    {
        if (isLocalPlayer)
            controllerPanel.SetActive(true);

        //DontDestroyOnLoad(this);

        base.OnStartClient();
        Debug.Log("Client Network Player start");
        StartPlayer();

        //Register();
        DominoLobbyManager.Instance.RegisterNetworkPlayer(this);
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
                DominoLobbyManager.Instance.AlterTurns();
            }
        }
    }

    [Server]
    public void EndTurn()
    {
        DominoLobbyManager.Instance.AlterTurns();
    }

    public void LocalEndTurn()
    {
        if (!isLocalPlayer)
            return;
        EndTurn();
    }

    public override void OnStartClient()
    {
        DontDestroyOnLoad(this);

        base.OnStartClient();
        Debug.Log("Client Network Player start");
        StartPlayer();

        Register();
        //DominoLobbyManager.Instance.RegisterNetworkPlayer(this);
    }

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
        controller.SetupLocalPlayer();
    }

    [Server]
    public void Register()
    {
        DominoLobbyManager.Instance.RegisterNetworkPlayer(this);
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
        //turnText = playerName +"'s turn.";
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
        DominoLobbyManager.Instance.DeregisterNetworkPlayer(this);
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
        UpdateTimeServer(curtime);
    }

    [Server]
    public void UpdateTimeServer(float curtime)
    {
        GameObject timerText = GameObject.FindWithTag("Timer");
        Text timer = timerText.GetComponent<Text>();
        timer.text = Mathf.Round(curtime).ToString();
    }
    private void OnChangeName(string n)
    {
        playerName = n;
    }

    public void SetTurn()
    {
        isTurn = true;
    }
}