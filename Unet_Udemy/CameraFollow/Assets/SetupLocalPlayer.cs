using System;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class SetupLocalPlayer : NetworkBehaviour {

    public Text namePrefab;
    public Text nameLabel;
    public Transform namePos;

    private string textBoxName = "";

    [SyncVar (hook = "OnChangeName")]
    public string pName = "player";

	void Start () 
	{
		if(isLocalPlayer)
		{
			GetComponent<PlayerController>().enabled = true;
            CameraFollow360.player = this.gameObject.transform;
		}
		else
		{
			GetComponent<PlayerController>().enabled = false;
		}

        GameObject canvas = GameObject.FindWithTag("MainCanvas");
        nameLabel = Instantiate(namePrefab, Vector3.zero, Quaternion.identity) as Text;
        nameLabel.transform.SetParent(canvas.transform); 
	}

	void Update()
	{
        Vector3 nameLabelPos = Camera.main.WorldToScreenPoint(namePos.position);
        nameLabel.transform.position = nameLabelPos;
	}

    private void OnGUI()
    {
        if (isLocalPlayer)
        {
            textBoxName = GUI.TextField(new Rect(25, 15, 100, 25), textBoxName);
            if (GUI.Button(new Rect(130, 15, 35, 25), "Set"))
                CmdChangeName(textBoxName);
        }
    }

    [Command]
    private void CmdChangeName(string newName)
    {
        pName = newName;
        nameLabel.text = pName;
    }

    private void OnChangeName(string newName)
    {
        pName = newName;
        nameLabel.text = pName;
    }
}
