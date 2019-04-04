using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SnakePlayerController : NetworkBehaviour
{
    // TODO: Done
    //       Put those variables in a game manager
    private float timeStep = 0f;
    private float speedUpTimer = 2500f;

    [HideInInspector]
    public Transform bodyHolder;

    private enum directionFacing {Up, Down, Right, Left };

    private directionFacing playerDirection;
    private int counter = 1;
    private SnakeTailController stc;
    TestGameManager tgm;

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
        tgm = TestGameManager.Instance;
    }

    void Start()
    {
        if (tgm != null)
        {
            timeStep = tgm.GetTimeStep();
            speedUpTimer = tgm.GetSpeedUpTimer();
        }
        else
            Debug.Log("Failed to retrieve t game manager");
            //CmdDebugLog("FAILED TO RETRIEVE T Game Manager.");

        CmdDebugLog("TimeStep " + timeStep);
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
        // Had to take away, it was causing the tail to leave the snake
        
        
        if (counter % speedUpTimer == 0 && timeStep > 0.04f)
        {
            SpeedUp();
        }

        counter++;
    }

    public void CancelStepUpdate()
    {
        CancelInvoke("CmdStepUpdate");
        Debug.Log("Canceled : CancelStepUpdate");
    }

    #region Commands
    [Command]
    public void CmdCancelStepUpdate()
    {       
        CancelInvoke("CmdStepUpdate");
        Debug.Log("Canceled : CmdCancelStepUpdate");
    }

    [Command]
    private void CmdStepUpdate()
    {
        Vector3 pos = transform.position;

        transform.Translate(direction);

        stc.MoveTail(pos, direction, bodyHolder);
    }

    [Command]
    public void CmdDebugLog(string s)
    {
        Debug.Log(s);
    }
    #endregion

    private void SpeedUp()
    {
        CancelStepUpdate();
        timeStep -= 0.01f;
        InvokeRepeating("CmdStepUpdate", 0, timeStep);
        CmdDebugLog("Speed up to : "+ timeStep);
    }

    private void SetDirection(Vector3 directionToFace)
    {
        direction = directionToFace;
    }

    public Vector3 GetDirection()
    {
        return direction;
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
