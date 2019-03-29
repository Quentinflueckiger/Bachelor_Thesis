using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TestPlayerController : NetworkBehaviour
{
    // TODO: Put those variables in a game manager
    public float timeStep = 0.1f;
    public float speedUpTimer = 2500;

    [HideInInspector]
    public Transform bodyHolder;

    private enum directionFacing {Up, Down, Right, Left };

    private directionFacing playerDirection;
    private int counter = 1;
    private SnakeTailController stc;

    #region Vector direction definition
    private Vector3 direction = new Vector3(0, 0, 0);
    private Vector3 directionUp = new Vector3(0, 1, 0);
    private Vector3 directionDown = new Vector3(0, -1, 0);
    private Vector3 directionRight = new Vector3(1, 0, 0);
    private Vector3 directionLeft = new Vector3(-1, 0, 0);
    #endregion

    private void Awake()
    {
        bodyHolder = new GameObject("Player").transform;
        transform.parent = bodyHolder;
    }

    void Start()
    {
        InvokeRepeating("CmdStepUpdate", 0, timeStep);
        stc = GetComponent<SnakeTailController>();
    }

    void Update()
    {      
        if (Input.GetKeyDown(KeyCode.UpArrow))
            GoUp();

        if (Input.GetKeyDown(KeyCode.DownArrow))
            GoDown();

        if (Input.GetKeyDown(KeyCode.RightArrow))
            GoRight();

        if (Input.GetKeyDown(KeyCode.LeftArrow))
            GoLeft();

        // TODO : Either keep it that way, or synchronized it over all the players
        if (counter % speedUpTimer == 0)
        {
            SpeedUp();
        }

        counter++;
    }

    #region Commands
    [Command]
    public void CmdCancelStepUpdate()
    {       
        CancelInvoke("CmdStepUpdate");       
    }

    [Command]
    private void CmdStepUpdate()
    {
        Vector3 pos = transform.position;

        transform.Translate(direction);

        stc.MoveTail(pos, direction, bodyHolder);
    }
    #endregion

    private void SetDirection(Vector3 directionToFace)
    {
        direction = directionToFace;
    }

    public Vector3 GetDirection()
    {
        return direction;
    }

    private void SpeedUp()
    {
        CmdCancelStepUpdate();
        timeStep -= 0.01f;
        InvokeRepeating("CmdStepUpdate", 0, timeStep);
    }

    #region Button functions
    public void GoUp()
    {
        if (playerDirection != directionFacing.Down)
        {
            playerDirection = directionFacing.Up;
            SetDirection(directionUp);
        }
            
    }
    public void GoDown()
    {
        if (playerDirection != directionFacing.Up)
        {
            playerDirection = directionFacing.Down;
            SetDirection(directionDown);
        }
    }
    public void GoRight()
    {
        if (playerDirection != directionFacing.Left)
        {
            playerDirection = directionFacing.Right;
            SetDirection(directionRight);
        }
    }
    public void GoLeft()
    {
        if (playerDirection != directionFacing.Right)
        {
            playerDirection = directionFacing.Left;
            SetDirection(directionLeft);
        }
    }
    #endregion
}
