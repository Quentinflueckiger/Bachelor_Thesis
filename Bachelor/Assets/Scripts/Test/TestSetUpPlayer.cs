using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.Events;

public class TestSetUpPlayer : NetworkBehaviour
{
    public Text nameTag;
    public string playerName;
    public Vector3 tagOffSet;

    public GameObject ctrPanel;
    private TestPlayerController tpc;
    private GameObject controllerPanel;
    private List<UnityAction> actionBtn = new List<UnityAction>();


    void Start()
    {
        tpc = GetComponent<TestPlayerController>();
        controllerPanel = GameObject.Find("ControllerPanel");

        if (isLocalPlayer)
        {
            tpc.enabled = true;
    
        }
        else
        {
            tpc.enabled = false;
            
        }
        if(!isServer)           
        {
            if (controllerPanel != null)
                controllerPanel.SetActive(true);

            ctrPanel = (GameObject)Instantiate(ctrPanel, controllerPanel.transform);

            InitActionList();
            
            InitButtonWithFunction(ctrPanel);
            
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
        actionBtn.Add(tpc.GoUp);
        actionBtn.Add(tpc.GoLeft);
        actionBtn.Add(tpc.GoRight);
        actionBtn.Add(tpc.GoDown);
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
        }
    }

    /*
    void OnGUI()
    {
        if (isLocalPlayer)
        {
            if (GUI.Button(new Rect(500, 300, 45, 25), "Up"))
                tpc.GoUp(); ;
            if (GUI.Button(new Rect(475, 325, 45, 25), "Left"))
                tpc.GoLeft();
            if (GUI.Button(new Rect(525, 325, 45, 25), "Right"))
                tpc.GoRight();
            if (GUI.Button(new Rect(500, 350, 45, 25), "Down"))
                tpc.GoDown();
        }
    }
    */
}
