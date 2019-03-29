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
            gameObject.SetActive(false);
            GetComponent<TestPlayerController>().CmdCancelStepUpdate();
        }
        else if (other.gameObject.tag == "Tail")
        {
            // TODO : Add code to check the color and steal this part
            gameObject.SetActive(false);
            GetComponent<TestPlayerController>().CmdCancelStepUpdate();
            Debug.Log("Hit tail");
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
