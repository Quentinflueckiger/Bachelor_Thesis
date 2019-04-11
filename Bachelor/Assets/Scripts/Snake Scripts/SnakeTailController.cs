using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SnakeTailController : MonoBehaviour
{
    // The prefab to be instanciated as tail element
    public GameObject boxPrefab;
    // List which stores transform of all tail element
    private List<Transform> tail = new List<Transform>();
    // Used as flag to notice when the snake eats a box
    private bool ate;
    // The material of the tail element to be spawned
    private Material spriteMaterial;
    // The position of the last spawned element
    private Vector3 oldPosition;

    public void MoveTail(Vector3 position, Vector3 direction, Transform bodyHolder)
    {
       
        // Add new element to the tail
        if (ate)
        {
            // TODO : Optimisation
            //        Use object pool
            // Load Prefab into the world, set it's material and parent
            GameObject box = (GameObject)Instantiate(boxPrefab,
                                                  position,
                                                  Quaternion.identity);
            box.GetComponent<SpriteRenderer>().material = spriteMaterial;
            box.transform.SetParent(bodyHolder);

            // Keep track of it in our tail list
            tail.Insert(0, box.transform);

            // Reset the flag
            ate = false;

            // TODO : Find a work around the vector3 == null 
            if (oldPosition == null)
                oldPosition = position;
        }

        float distanceMoved = Vector3.Distance(position, oldPosition);

        if (distanceMoved > 2.95f && tail.Count > 0)
        {
            tail.Last().GetComponent<BoxCollider2D>().enabled = false;
            tail.Last().position = position;

            tail.Insert(0, tail.Last());
            tail.RemoveAt(tail.Count - 1);

            if (tail.Count > 2)
                tail[2].GetComponent<BoxCollider2D>().enabled = true;

            oldPosition = position;
        }
    }

    public void SetAte(bool status)
    {
        ate = status;
    }

    public void SetMaterial(Material material)
    {
        spriteMaterial = material;
    }

    public List<Transform> GetTail()
    {
        return tail;
    }
    #region Deprecated
    // Add parameter color, the color of the box which will be used later for improved gameplay
    // Fix spawn problem with the collider, as the object is created within
    //        an other gameobjec which has a collider it automatically uses it.
    /*public void AddToTail()
    {
        if (tailPosition == null)
            tailPosition = oldPosition;
        newTailObject = Instantiate(boxPrefab, tailPosition.position, Quaternion.identity, tailHolder.transform);  
        tailPosition = newTailObject.transform;
        
    }*/
     /*else if (tail.Count > 0 && counter % 3 == 0)
        {
            tail.Last().position = position;

            tail.Insert(0, tail.Last());
            tail.RemoveAt(tail.Count - 1);
        }
        counter++;*/
    #endregion
}
