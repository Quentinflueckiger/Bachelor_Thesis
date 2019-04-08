using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BoxSpawner : NetworkBehaviour
{
    public GameObject box;
    public Material[] spriteMaterial;

    // TODO: Put those variables in a game manager
    private int maxBoxes;
    private int spawnInterval;
    private bool isSpawning = false;

    private int halfWidth;
    private int halfHeight;

    SnakeGameManager sgm;     

    public override void OnStartServer()
    {
        sgm = SnakeGameManager.Instance;

        if (sgm == null)
        {
            Debug.Log("Couldn't find Game Manager");
            return;
        }
        maxBoxes = sgm.GetMaxBoxes();
        spawnInterval = sgm.GetBoxSpawnInterval();
        halfWidth = sgm.GetX();
        halfHeight = sgm.GetY();
        //InvokeRepeating("SpawnBoxes", 0, spawnInterval);
    }

    public void Update()
    {
        // Check if the game is running with the game manager
        if (sgm.GetGameStatus() && !isSpawning)
            InvokeRepeating("SpawnBoxes", 0, spawnInterval);
        if (!sgm.GetGameStatus() && isSpawning)
            CancelInvoke("SpawnBoxes");
    }

    private void SpawnBoxes()
    {
        isSpawning = true;
        for (int i = 0; i < maxBoxes - GetCurrentAmountOfBoxes(); i++)
        {
            SpawnBox();
        }
    }

    private void SpawnBox()
    {
        // TODO : Create a pool of boxes to use instead of destroying and creating new each time
        // DONE
        //        Check that the spawnPosition is empty before spawning the new box 
        
        Vector3 spawnPosition = new Vector3(Random.Range(-halfWidth, halfWidth), 
                                            Random.Range(-halfHeight, halfHeight),
                                            0);

        if (CheckIfEmptyAtPosition(spawnPosition))
        {
            int i = Random.Range(0, spriteMaterial.Length);
            GameObject newBox = Instantiate(box, spawnPosition, transform.rotation, transform);
            newBox.GetComponent<SpriteRenderer>().material = spriteMaterial[i];
            NetworkServer.Spawn(newBox);
        }
           
    }

    private static int GetCurrentAmountOfBoxes()
    {
        // TODO: Try with a public static int which gets incr or decr when a box spawns or get eaten
        return GameObject.FindGameObjectsWithTag("Box").Length;
    }

    private bool CheckIfEmptyAtPosition(Vector3 position)
    {
        // Raycast doesn't work with 2D collider
        // Have to use raycast2d
        // TODO: Screenshot for doc
        /*RaycastHit hit;
        Ray ray = new Ray(position + new Vector3(0,0,-5), Vector3.forward*50);

        if (Physics.Raycast(ray, out hit))
        {
            Debug.Log("TOUCH");
        }
        */
        RaycastHit2D hit = Physics2D.Raycast(position+ new Vector3(0,0,-5), Vector3.forward);

        if ( hit.collider != null)
        {
            // Debug.Log("NAME COLL: " + hit.collider.name);
            return false;
        }

        return true;
    }

    // Idea not kept, used Raycast2D
    private bool CheckIfEmptyWithMatrix(int x, int y)
    {
        // TODO : Create a matrix from Width x Height and check the surroundings from x,y
        return false;
    }

}

