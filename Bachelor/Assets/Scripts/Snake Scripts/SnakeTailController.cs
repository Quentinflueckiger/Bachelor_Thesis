using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SnakeTailController : MonoBehaviour
{
    //public int offSet = 1;
    public GameObject boxPrefab;
    //public GameObject tailHolder;
    private List<Transform> tail = new List<Transform>();
    //private Transform tailPosition;
    //public GameObject newTailObject;
    //private Transform oldPosition;
    private bool ate;
    private Material spriteMaterial;
    //private int counter = 0;
    private Vector3 oldPosition;

    void Start()
    {
        //oldPosition = transform;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = newTailObject.transform.position;
    }

    // TODO : Add parameter color, the color of the box which will be used later for improved gameplay
    // TODO : Fix spawn problem with the collider, as the object is created within
    //        an other gameobjec which has a collider it automatically uses it.
    /*public void AddToTail()
    {
        if (tailPosition == null)
            tailPosition = oldPosition;
        newTailObject = Instantiate(boxPrefab, tailPosition.position, Quaternion.identity, tailHolder.transform);  
        tailPosition = newTailObject.transform;
        
    }*/

    public void MoveTail(Vector3 position, Vector3 direction, Transform bodyHolder)
    {
       
        // Add new element to the tail
        if (ate)
        {
            // TODO : Use object pool
            // Load Prefab into the world
            GameObject g = (GameObject)Instantiate(boxPrefab,
                                                  position,
                                                  Quaternion.identity);
            g.GetComponent<SpriteRenderer>().material = spriteMaterial;
            //g.GetComponent<BoxCollider2D>().enabled = false;
            g.transform.SetParent(bodyHolder);

            // Keep track of it in our tail list
            tail.Insert(0, g.transform);

            // Reset the flag
            ate = false;

            // TODO : find a work around the vector3 == null 
            if (oldPosition == null)
                oldPosition = position;
        }

        /*else if (tail.Count > 0 && counter % 3 == 0)
        {
            tail.Last().position = position;

            tail.Insert(0, tail.Last());
            tail.RemoveAt(tail.Count - 1);
        }
        counter++;*/

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
}
