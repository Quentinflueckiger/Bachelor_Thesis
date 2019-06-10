using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DominoPTest : MonoBehaviour
{
    bool isLocalPlayer = false;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }
       
    }

    public void SetupLocalPlayer()
    {
        //add color to your player
        isLocalPlayer = true;
    }

    public void TurnStart()
    {
        if (isLocalPlayer)
        {
            //spawn or enable player
        }
    }

    public void TurnEnd()
    {
        if (isLocalPlayer)
        {
            // unspawn or disable player
        }
    }

}