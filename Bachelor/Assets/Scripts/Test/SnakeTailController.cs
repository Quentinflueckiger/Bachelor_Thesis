using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeTailController : MonoBehaviour
{

    public GameObject boxPrefab;
    public GameObject tailHolder;
    private List<Transform> tail = new List<Transform>();
    private Transform tailPosition;
    public GameObject newTailObject;
    private Transform oldPosition;
    void Start()
    {
        oldPosition = transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = newTailObject.transform.position;
    }

    // TODO : Add parameter color, the color of the box which will be used later for improved gameplay
    // TODO : Fix spawn problem with the collider, as the object is created within
    //        an other gameobjec which has a collider it automatically uses it.
    public void AddToTail()
    {
        if (tailPosition == null)
            tailPosition = oldPosition;
        newTailObject = Instantiate(boxPrefab, tailPosition.position, Quaternion.identity, tailHolder.transform);  
        tailPosition = newTailObject.transform;
        
    }

    public void MoveTail()
    {

    }

    public void SetOldPos(Transform head)
    {
        oldPosition = head;
    }
}
