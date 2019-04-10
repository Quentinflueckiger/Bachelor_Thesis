using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(NetworkManager))]
public class SnakeGameManager : MonoBehaviour
{
    [HideInInspector]
    public NetworkManager manager;

    public float timeStep = 0.1f;
    public float speedUpTimer = 2500;
    //private int stepCounter = 1;
    public float endGameTimer = 40000.0f;
    public Text timerText;

    public int maxBoxes = 10;
    public int boxSpawnInterval = 5;
    public int borderOffSetSize = 3;

    [SerializeField]
    GameObject scoreboard;

    private int halfWidth = 81;
    private int halfHeight = 43;
    private bool gameOver = false;
    private bool endGame = false;
    private bool gameStart = false;

    private List<GameObject> players = new List<GameObject>();
    private List<GameObject> playersAlive = new List<GameObject>();

    // Used as singleton
    public static SnakeGameManager Instance;

    void Awake()
    {
        Instance = this;
        this.manager = this.GetComponent<NetworkManager>();
        timerText.text = "";
    }

    private void Update()
    {
        if (players.Count > 0 && !gameStart)
        {
            if (!gameOver && !endGame)
                StartGame();
        }

        if (gameStart)
        {
            if (!gameOver && !endGame)
            {
                Count();
            }
        }
            
    }

    #region Getters
    public float GetTimeStep()
    {
        return timeStep;
    }

    public float GetSpeedUpTimer()
    {
        return speedUpTimer;
    }

    public int GetMaxBoxes()
    {
        return maxBoxes;
    }

    public int GetBoxSpawnInterval()
    {
        return boxSpawnInterval;
    }

    public int GetBorderOffSetSize()
    {
        return borderOffSetSize;
    }

    public int GetX()
    {
        return halfWidth - borderOffSetSize;
    }

    public int GetY()
    {
        return halfHeight - borderOffSetSize;
    }
    #endregion

    public void AddPlayer(GameObject player)
    {
        players.Add(player);
        playersAlive.Add(player);
        Debug.Log("Player added, number of players : " + players.Count);
    }

    public void Remove(GameObject player)
    {
        players.Remove(player);
    }

    public void RemovePlayer(GameObject player)
    {
        playersAlive.Remove(player);
        if (playersAlive.Count < 1)
            GameOver();
    }

    private void StartGame()
    {
        gameStart = true;
        gameOver = false;
        endGame = false;
    }

    private void GameOver()
    {
        gameOver = true;
        Debug.Log("Game Over");
        gameStart = false;
        // TODO : Add what happens when the game is ended due to lack of skill of the players

        // Show score board
        // TODO : UNITY_STANDALONE
#if UNITY_EDITOR || UNITY_STANDALONE
        scoreboard.SetActive(true);
#endif
    }

    private void EndGame()
    {
        endGame = true;
        Debug.Log("Game ended");
        gameStart = false;
        DisableTimerText();

        // TODO : Add what happens when the game is ended due to a natural cause
        foreach (GameObject player in playersAlive)
        {
            player.GetComponent<SnakePlayerController>().CmdOnEndGame();
            player.GetComponent<SnakePlayerController>().OnEndGame();
        }
        // Show score board
        // TODO : UNITY_STANDALONE
#if UNITY_EDITOR || UNITY_STANDALONE
        scoreboard.SetActive(true);
#endif

    }

    private void Count()
    {
        if (endGameTimer <= 0)
        {
            EndGame();
        }
        else
        {
            SetTimerText();
            endGameTimer -= Time.deltaTime;
        }

    }

    private void SetTimerText()
    {
        string minutes = ((int)endGameTimer / 60).ToString();
        string seconds = (endGameTimer % 60).ToString("f2");
        timerText.text = minutes + " : " + seconds;
    }

    private void DisableTimerText()
    {
        timerText.gameObject.SetActive(false);
    }

    public bool GetGameStatus()
    {
        return gameStart;
    }

    public GameObject[] GetPlayers()
    {
        return players.ToArray();
    }

    public List<GameObject> GetPlayersList()
    {
        return players;
    }
}
