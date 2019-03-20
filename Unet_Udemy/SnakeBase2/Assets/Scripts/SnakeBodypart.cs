using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeBodypart : MonoBehaviour {

    //TODO: Make a list of snake parts and reuse them rather then making new ones all the time (Hard)

    float lifeTimeRemaining = 0;

    private void Update()
    {
        ManualLifeTimeCounter();
    }

    private void ManualLifeTimeCounter()
    {
        lifeTimeRemaining -= Time.deltaTime;

        if (lifeTimeRemaining < 0)
            Destroy(gameObject);
    }

    public void AddLifeTime(float time)
    {
        lifeTimeRemaining += time;
    }
}
