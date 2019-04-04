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

    public GameObject ctrPanel;
    private SnakePlayerController spc;
    private GameObject controllerPanel;
    private GameObject connectPanel;
    private List<UnityAction> actionBtn = new List<UnityAction>();

    void Start()
    {
        spc = GetComponent<SnakePlayerController>();
        controllerPanel = GameObject.Find("ControllerPanel");
        connectPanel = GameObject.Find("ConnectPanel");
        //NetworkManagerHUD hud = FindObjectOfType<NetworkManagerHUD>();

        if (isLocalPlayer)
        {
            spc.enabled = true;

            /*if (hud != null)
                hud.showGUI = false;
            */
        }

        else
        {
            spc.enabled = false;      
        }

        if (!isServer)
        {
            if (controllerPanel != null)
                controllerPanel.SetActive(true);

            ctrPanel = (GameObject)Instantiate(ctrPanel, controllerPanel.transform);

            InitActionList();

            InitButtonWithFunction(ctrPanel);

            if (connectPanel != null)
                connectPanel.SetActive(false);
        }
        else
        {
            if (controllerPanel != null)
                controllerPanel.SetActive(false);
        }

        // TODO : Transfert user name from lobby
        Transform canvas = GameObject.Find("PlayerNames").transform;
        nameTag = Instantiate(nameTag, canvas);

        nameTag.text = playerName;

    }

    private void Update()
    {
        Vector3 newPosition = Camera.main.WorldToScreenPoint(transform.position + tagOffSet);
        nameTag.transform.position = newPosition;
    }

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
            // TODO : Done
            //        Change method added to the correct ones
            Button btn = buttons[i];           
            btn.onClick.AddListener(actionBtn[i]);
        }
    }

    public void OnDestroy()
    {
        if (nameTag != null)
        {
            Destroy(nameTag.gameObject);
            Destroy(ctrPanel.gameObject);       // Problem
            /*  
             *  Destroying assets is not permitted to avoid data loss.
             *  If you really want to remove an asset use DestroyImmediate (theObject, true);
            */
        }
    }
}

