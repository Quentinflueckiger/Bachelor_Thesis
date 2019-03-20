using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeMovement : MonoBehaviour
{
    Vector3 direction = new Vector3(0, 0, 0);

    // Use this for initialization
    void Start()
    {
        //Start our step by step update
        InvokeRepeating("StepUpdate", 0, Snake.timeStep);
    }

    // Update is called once per frame
    private void Update()
    {
        //TODO: replace vectors with presets    (Easy)
        //TODO: stop player from moving "back"  (Easy)
        //TODO: extract function                (Easy)
        if (Input.GetKeyDown(KeyCode.UpArrow))
            direction = new Vector3(0, 1, 0);

        if (Input.GetKeyDown(KeyCode.DownArrow))
            direction = new Vector3(0, -1, 0);

        if (Input.GetKeyDown(KeyCode.LeftArrow))
            direction = new Vector3(-1, 0, 0);

        if (Input.GetKeyDown(KeyCode.RightArrow))
            direction = new Vector3(1, 0, 0);
    }


    void StepUpdate()
    {
        transform.position += direction;
    }
}
