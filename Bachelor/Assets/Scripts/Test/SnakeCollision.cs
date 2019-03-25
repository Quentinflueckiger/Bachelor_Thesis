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
            GetComponent<TestPlayerController>().CancelStepUpdate();
        }
        else if (other.gameObject.tag == "Tail")
        {
            // TODO : Add code to check the color and steal this part
            //gameObject.SetActive(false);
            //GetComponent<TestPlayerController>().CancelStepUpdate();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Box")
        {
            AddBoxToSnake(other.gameObject);

            Destroy(other.gameObject);
        }
    }

    private void AddBoxToSnake(GameObject box)
    {
        // TODO : Add box to tail of snake
        Debug.Log("Box eaten! MIAM !");
        GetComponent<SnakeTailController>().AddToTail();
    }
}
