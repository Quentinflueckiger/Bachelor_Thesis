using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DominoGameManager : MonoBehaviour
{
    public GameObject dominoPrefab;
    public GameObject controllerPanel;

    public List<DominoCard> dominoCards = new List<DominoCard>();
    private List<DominoCard> dominoAvailable;

    private List<GameObject> players = new List<GameObject>();
    private bool endGame = false;
    private bool gameStart = false;

    public static DominoGameManager Instance;

    private void Awake()
    {
        Instance = this;
        // Put in start game ?
        dominoAvailable = new List<DominoCard>(dominoCards);
    }
    
    void Start()
    {
        int x = 6;
        Debug.Log("domino list : "+dominoAvailable.Count);
        for (int i = 0; i < x; i++)
        {
            GameObject dominoToInstantiate = Instantiate(dominoPrefab, new Vector3(),Quaternion.identity);
            int random = UnityEngine.Random.Range(0, dominoAvailable.Count - 1);
            dominoToInstantiate.GetComponent<DominoDisplay>().SetDominoCard(dominoAvailable[random]);
            dominoToInstantiate.transform.parent = controllerPanel.transform;
            dominoAvailable.RemoveAt(random);
        }
        Debug.Log("domino list : " + dominoAvailable.Count);
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
    }
}
