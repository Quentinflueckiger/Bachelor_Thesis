using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(NetworkManager))]
public class TestGameManager : MonoBehaviour
{
    [HideInInspector]
    public NetworkManager manager;

    public float timeStep = 0.1f;
    public float speedUpTimer = 2500;
    private int stepCounter = 1;

    public int maxBoxes = 10;
    public int boxSpawnInterval = 5;
    public int borderOffSetSize = 3;
    private int halfWidth = 81;
    private int halfHeight = 43;

    private List<GameObject> players = new List<GameObject>();

    // Used as singleton
    public static TestGameManager Instance;

    void Awake()
    {
        Instance = this;
        this.manager = this.GetComponent<NetworkManager>();
    }

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

    public void AddPlayer(GameObject player)
    {
        players.Add(player);
    }

    public void Remove(GameObject player)
    {
        players.Remove(player);
    }
}
