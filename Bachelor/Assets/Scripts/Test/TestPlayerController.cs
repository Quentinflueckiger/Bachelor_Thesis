using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayerController : MonoBehaviour
{

    public float timeStep = 0.1f;

    private Vector3 direction = new Vector3(0, 0, 0);

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("StepUpdate", 0, timeStep);             //Snake.timeStep
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
            direction = new Vector3(0, 1, 0);

        if (Input.GetKeyDown(KeyCode.DownArrow))
            direction = new Vector3(0, -1, 0);

        if (Input.GetKeyDown(KeyCode.LeftArrow))
            direction = new Vector3(-1, 0, 0);

        if (Input.GetKeyDown(KeyCode.RightArrow))
            direction = new Vector3(1, 0, 0);
    }

    public void CancelStepUpdate()
    {
        CancelInvoke("StepUpdate");
    }
    private void StepUpdate()
    {
        transform.position += direction;
    }
}
