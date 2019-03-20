using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Snake : NetworkBehaviour
{
    public static float timeStep = 0.1f;
    private static int numberOfPlayers = 0;

    [SyncVar]
    public int lenght = 1;

    [HideInInspector]
    public Transform bodyHolder;

    public Color color;
    public int maxNumberOfPlayer;
    public Vector3[] startPosition;

    // Use this for initialization
    void Awake()
    {
        bodyHolder = new GameObject("Snake").transform;
        transform.parent = bodyHolder;

        color = GetComponent<SpriteRenderer>().color;

        if (numberOfPlayers > maxNumberOfPlayer)
        {
            Destroy(gameObject);
            return;
        }

        transform.position = startPosition[numberOfPlayers];
        numberOfPlayers++;

    }

    private void Start()
    {
        if(!isLocalPlayer)
        {
            GetComponent<SnakeMovement>().enabled = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        //Player Died
        var snakeParts = bodyHolder.GetComponentsInChildren<SnakeBodypart>();

        foreach (var part in snakeParts)    //TODO: extract and use deligates to remove duplicate code (Hard)
            part.enabled = false;

        gameObject.SetActive(false);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Apple")
        {
            lenght += 3; //TODO: Get value from apple? (Medium)

            var snakeParts = bodyHolder.GetComponentsInChildren<SnakeBodypart>();

            foreach (var part in snakeParts)    //TODO: extract and use deligates to remove duplicate code (Hard)
                part.AddLifeTime(3 * timeStep);         //Calculate from timeScale (Medium)

            Destroy(other.gameObject);
        }
    }
}
