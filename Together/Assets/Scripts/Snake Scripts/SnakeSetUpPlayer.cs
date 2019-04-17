using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class SnakeSetUpPlayer : NetworkBehaviour
{
    public Text nameTag;
    [SyncVar(hook = "OnChangeName"), HideInInspector]
    public string playerName = "player name"; 
    [SyncVar (hook = "OnChangeColour"), HideInInspector]
    public Color playerColour = Color.green;
    public Vector3 tagOffSet;
    public GameObject controller;
    public GameObject canvasController;

    //public GameObject ctrPanel;
    private SnakePlayerController spc;
    private GameObject controllerPanel;
    //private GameObject controllerHolder;
    //private GameObject controller;
    //private GameObject connectPanel;
    //private List<UnityAction> actionBtn = new List<UnityAction>();
    private SnakeGameManager sgm;

    void Start()
    {
        spc = GetComponent<SnakePlayerController>();
        controllerPanel = GameObject.Find("ControllerPanel");
        //controllerHolder = GameObject.Find("ControllerHolder");

        sgm = SnakeGameManager.Instance;
        if (sgm == null)
            Debug.Log("Failed to retrieve game manager.");

        // Enable the player controller script if it is the local player
        if (isLocalPlayer)
        {
            spc.enabled = true;
            controller.SetActive(true);
            canvasController.SetActive(true);
        }

        else
        {
            spc.enabled = false;
            canvasController.SetActive(false);
        }

        if (isServer)
        {
            if (controllerPanel != null)
                controllerPanel.SetActive(false);
            controller.gameObject.transform.localScale = new Vector3(0, 0, 0);
        }

        // TODO : Transfert user name from lobby
        // Instantiate the text object which holds the player's name/username
        Transform canvas = GameObject.Find("PlayerNames").transform;
        nameTag = Instantiate(nameTag, canvas);

        nameTag.text = playerName;
        this.GetComponent<SpriteRenderer>().color = playerColour;

        // Parent the controller to the Controller object which is a child of the main canvas
        //controller.transform.SetParent(controllerHolder.transform);
        //controller.transform.position.Set(0, 0, 0);
    }

    private void Update()
    {
        // Make the name text object follow the position of the player
        Vector3 newPosition = Camera.main.WorldToScreenPoint(transform.position + tagOffSet);
        nameTag.transform.position = newPosition;
    }

    private void OnChangeName(string n)
    {
        playerName = n;
        nameTag.text = playerName;
    }

    private void OnChangeColour(Color n)
    {
        playerColour = n;
        this.GetComponent<SpriteRenderer>().color = playerColour;
        // TODO : DONE
        //        Add code to change colour of snake head
    }

    public string GetPlayerName()
    {
        return playerName;
    }

    public void OnDestroy()
    {
        if (nameTag != null)
        {
            Destroy(nameTag.gameObject);
            controller.SetActive(false);
            sgm.Remove(this.gameObject);
            Destroy(transform.parent.gameObject);
            //Destroy(ctrPanel.gameObject);       // Problem
            /*  
             *  Destroying assets is not permitted to avoid data loss.
             *  If you really want to remove an asset use DestroyImmediate (theObject, true);
            */
        }
    }
    /*
     * void OnGUI()
    {
        if (isLocalPlayer)
        {
            if (GUI.Button(new Rect(250, 150, 100, 50), "Up"))
                spc.GoUp(); ;
            if (GUI.Button(new Rect(100, 100, 50, 100), "Left"))
                spc.GoLeft();
            if (GUI.Button(new Rect(400, 100, 50, 100), "Right"))
                spc.GoRight();
            if (GUI.Button(new Rect(250, 250, 100, 50), "Down"))
                spc.GoDown();
        }
    }*/
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

