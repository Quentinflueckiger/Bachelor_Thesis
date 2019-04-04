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
    private List<GameObject> players = new List<GameObject>();

    // Used as singleton
    public static TestGameManager Instance;

    void Awake()
    {
        Instance = this;
        this.manager = this.GetComponent<NetworkManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float GetTimeStep()
    {
        return timeStep;
    }

    public float GetSpeedUpTimer()
    {
        return speedUpTimer;
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
