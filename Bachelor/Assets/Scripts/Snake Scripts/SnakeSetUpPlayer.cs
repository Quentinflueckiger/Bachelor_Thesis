using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.Events;

public class SnakeSetUpPlayer : NetworkBehaviour
{
    public Text nameTag;
    public string playerName;
    public Vector3 tagOffSet;
    public GameObject controller;

    //public GameObject ctrPanel;
    private SnakePlayerController spc;
    private GameObject controllerPanel;
    //private GameObject controller;
    //private GameObject connectPanel;
    //private List<UnityAction> actionBtn = new List<UnityAction>();

    void Start()
    {
        spc = GetComponent<SnakePlayerController>();
        controllerPanel = GameObject.Find("ControllerPanel");
        //NetworkManagerHUD hud = FindObjectOfType<NetworkManagerHUD>();

        // Enable the player controller script if it is the local player
        if (isLocalPlayer)
        {
            spc.enabled = true;
            controller.SetActive(true);
            /*if (hud != null)
                hud.showGUI = false;
            */
        }

        else
        {
            spc.enabled = false;      
        }

        if (isServer)
        { 
            if (controllerPanel != null)
                controllerPanel.SetActive(false);
        }

        // TODO : Transfert user name from lobby
        // Instantiate the text object which holds the player's name/username
        Transform canvas = GameObject.Find("PlayerNames").transform;
        nameTag = Instantiate(nameTag, canvas);

        nameTag.text = playerName;

        // Parent the controller to the Controller object which is a child of the main canvas
        controller.transform.SetParent(controllerPanel.transform);
        controller.transform.position.Set(0, 0, 0);

    }

    private void Update()
    {
        // Make the name text object follow the position of the player
        Vector3 newPosition = Camera.main.WorldToScreenPoint(transform.position + tagOffSet);
        nameTag.transform.position = newPosition;
    }

    public void OnDestroy()
    {
        if (nameTag != null)
        {
            Destroy(nameTag.gameObject);
            controller.SetActive(false);
            //Destroy(ctrPanel.gameObject);       // Problem
            /*  
             *  Destroying assets is not permitted to avoid data loss.
             *  If you really want to remove an asset use DestroyImmediate (theObject, true);
            */
        }
    }

    #region Deprecated code
    /*
    private void InitActionList()
    {
        actionBtn.Add(spc.GoUp);
        actionBtn.Add(spc.GoLeft);
        actionBtn.Add(spc.GoRight);
        actionBtn.Add(spc.GoDown);
    }
    private void InitButtonWithFunction(GameObject panel)
    {
        Button[] buttons = ctrPanel.GetComponentsInChildren<Button>();
        for (int i = 0; i < buttons.Length; i++)
        {
            // Done
            //        Change method added to the correct ones
            Button btn = buttons[i];
            btn.onClick.AddListener(actionBtn[i]);
        }
    }
    */
    #endregion
}

