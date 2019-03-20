using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class AppleSpawner : NetworkBehaviour
{
    //Clean code!
    public int maxApples = 10;
    public GameObject apple;
    public int x = 80;          //TODO: Move to a Game Controller (Medium)
    public int y = 42;
    public int spawnInterval = 5;

	public override void OnStartServer() {
        InvokeRepeating("SpawnApples", 0, spawnInterval); 
    }
	

	void SpawnApples()
    {
        for (int i = 0; i < maxApples - GetCurrentAmountOfApples(); i++)
            SpawnApple();
    }


    private void SpawnApple()
    {
        Vector3 spawnPos = new Vector3(Random.Range(-x, x), Random.Range(-y, y), 0);
        GameObject newApple = Instantiate(apple, spawnPos, transform.rotation, transform);
        NetworkServer.Spawn(newApple);
    }


    private static int GetCurrentAmountOfApples()
    {
        return GameObject.FindGameObjectsWithTag("Apple").Length;
    }
}
