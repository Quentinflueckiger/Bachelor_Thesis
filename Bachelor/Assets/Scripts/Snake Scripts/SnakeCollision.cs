using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeCollision : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Wall")
        {
            // Only death awaits
            GetComponent<SnakePlayerController>().CancelStepUpdate();
            // TODO : Hide on server
            this.gameObject.SetActive(false);
            GetComponent<SnakePlayerController>().CmdDebugLog("Hit with " + other.collider.name);
        }
        else if (other.gameObject.tag == "Tail")
        {
            // TODO : Add code to check the color and steal this part
            GetComponent<SnakePlayerController>().CmdCancelStepUpdate();
            //GetComponent<SnakePlayerController>().CmdDebugLog("Material of collided object : " +
            //                                                    other.gameObject.GetComponent<SpriteRenderer>().material.name);
            // TODO : Hide on server
            gameObject.SetActive(false);
            GetComponent<SnakePlayerController>().CmdDebugLog("Hit tail");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Box")
        {
            // TODO : Different value for spawned boxes ?
            AddBoxToSnake(other.gameObject);

            // TODO : Use object pool
            Destroy(other.gameObject);
        }
    }

    private void AddBoxToSnake(GameObject box)
    {
        // TODO : Done : Add box to tail of snake
        //               Use box to send color
        GetComponent<SnakeTailController>().SetMaterial(box.GetComponent<SpriteRenderer>().material);
        GetComponent<SnakeTailController>().SetAte(true);
    }
}
