using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeCollision : MonoBehaviour
{

    private SnakePlayerController spc;

    private void Awake()
    {
        spc = GetComponent<SnakePlayerController>();
    }
    private void Start()
    {
        if (spc == null)
            Debug.Log("Failed to retrieve SnakePlayerController");
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Wall")
        {
            // Only death awaits
            if (spc != null)
                spc.CmdOnDeath(other.collider.name);
            else
                Debug.Log("SnakePlayerController not assigned");
        }
        else if (other.gameObject.tag == "Tail")
        {
            // TODO : Add code to check the color and steal this part
            if (spc != null)
            {
                spc.CmdOnDeath(other.collider.name);
                spc.OnDeath(other.collider.name);

            }      
            else
                Debug.Log("SnakePlayerController not assigned");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Box")
        {
            // TODO : Different value for spawned boxes ?
            AddBoxToSnake(other.gameObject);

            // TODO : Optimisation
            //        Use object pool
            Destroy(other.gameObject);
        }
    }

    private void AddBoxToSnake(GameObject box)
    {
        // Done  
        //      Add box to tail of snake
        //      Use box to send color
        GetComponent<SnakeTailController>().SetMaterial(box.GetComponent<SpriteRenderer>().material);
        GetComponent<SnakeTailController>().SetAte(true);
    }
}
