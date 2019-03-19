using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : MonoBehaviour
{
    private enum direction {Up, Down, Right, Left };
    direction playerDirection;
    private float speed;                    // Change to get a global speed from the game manager
    private Transform playerGfx;

    private void Start()
    {
        playerDirection = direction.Right;
        speed = 0.05f;
        playerGfx = transform.Find("GFX").transform;
    }

    void Update()
    {

        switch (playerDirection)
        {
            case direction.Up:
                MoveForward(0, 1);
                break;
            case direction.Down:
                MoveForward(0, -1);
                break;
            case direction.Right:
                MoveForward(1, 0);
                break;
            case direction.Left:
                MoveForward(-1, 0);
                break;
            default:
                Debug.Log("Unexpected switch case.");
                break;
        }

    }

    private void MoveForward(float x, float y)
    {
        transform.Translate(x * speed, y * speed, 0);
    }

    private void FaceDirection(direction faceDirection)
    {
        switch (faceDirection)
        {
            case direction.Up:
                playerGfx.rotation = Quaternion.Euler(0f, 0f, 90f);
                break;
            case direction.Down:
                playerGfx.rotation = Quaternion.Euler(0f, 0f, -90f);
                break;
            case direction.Right:
                playerGfx.rotation = Quaternion.Euler(0f, 0f, 0);
                break;
            case direction.Left:
                playerGfx.rotation = Quaternion.Euler(0f, 0f, 180);
                break;
            default:
                Debug.Log("Unexpected switch case.");
                break;
        }
    }

    #region Button functions
    public void GoUp()
    {
        if (playerDirection != direction.Down)
        {
            playerDirection = direction.Up;
            FaceDirection(playerDirection);
        }
            
    }
    public void GoLeft()
    {
        if (playerDirection != direction.Right)
        {
            playerDirection = direction.Left;
            FaceDirection(playerDirection);
        }
    }
    public void GoRight()
    {
        if (playerDirection != direction.Left)
        {
            playerDirection = direction.Right;
            FaceDirection(playerDirection);
        }
    }
    public void GoDown()
    {
        if (playerDirection != direction.Up)
        {
            playerDirection = direction.Down;
            FaceDirection(playerDirection);
        }
    }
    #endregion
}
