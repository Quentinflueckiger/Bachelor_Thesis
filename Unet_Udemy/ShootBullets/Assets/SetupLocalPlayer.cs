﻿using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class SetupLocalPlayer : NetworkBehaviour {

	public Text namePrefab;
	public Text nameLabel;
	public Transform namePos;
    public Slider healthPrefab;
    public Slider health;
    public GameObject explosion;
    NetworkStartPosition[] spawnPos;
	string textboxname = "";
	string colourboxname = "";


	//[SyncVar (hook = "OnChangeName")]
	public string pName = "player";

	//[SyncVar (hook = "OnChangeColour")]
	public string pColour = "#ffffff";

    //[SyncVar (hook = "OnChangeHealth")]
    public int healthValue = 100;

    // Syncvar not working since 07.03.2019, working again the week after ...
    // It makes the client freeze and get stuck that way
    // Using Rpc instead of syncvar from now on

    [ClientRpc]
    void RpcOnChangeName (string n)
    {
		pName = n;
		nameLabel.text = pName;
    }

    [ClientRpc]
    void RpcOnChangeColour (string n)
    {
		pColour = n;
		Renderer[] rends = GetComponentsInChildren<Renderer>( );

        foreach( Renderer r in rends )
        {
         	if(r.gameObject.name == "BODY")
            	r.material.SetColor("_Color", ColorFromHex(pColour));
        }
    }

    [ClientRpc]
    private void RpcOnChangeHealth (int n)
    {
        healthValue = n;
        health.value = healthValue;
    }

    [ClientRpc]
    private void RpcRespawn()
    {
        if (!isLocalPlayer) return;
        if (spawnPos != null && spawnPos.Length > 0)
        {
            this.transform.position = spawnPos[Random.Range(0, spawnPos.Length)].transform.position;
        }
    }

	[Command]
	public void CmdChangeName(string newName)
	{
		pName = newName;
		nameLabel.text = pName;
        RpcOnChangeName(newName);
	}

	[Command]
	public void CmdChangeColour(string newColour)
	{
		pColour = newColour;
		Renderer[] rends = GetComponentsInChildren<Renderer>( );

        foreach( Renderer r in rends )
        {
         	if(r.gameObject.name == "BODY")
            	r.material.SetColor("_Color", ColorFromHex(pColour));
        }
        RpcOnChangeColour(newColour);
	}

    [Command]
    public void CmdChangeHealth(int hitValue)
    {
        healthValue = healthValue + hitValue;
        health.value = healthValue;

        if (health.value <= 0)
        {
            GameObject e = Instantiate(explosion, this.transform.position, Quaternion.identity);
            NetworkServer.Spawn(e);
            this.GetComponent<Rigidbody>().velocity = Vector3.zero;
            RpcRespawn();
            healthValue = 100;
        }

        RpcOnChangeHealth(healthValue);

    }

	void OnGUI()
	{
		if(isLocalPlayer)
		{
			textboxname = GUI.TextField (new Rect (25, 15, 100, 25), textboxname);
			if(GUI.Button(new Rect(130,15,35,25),"Set"))
				CmdChangeName(textboxname);

			colourboxname = GUI.TextField (new Rect (170, 15, 100, 25), colourboxname);
			if(GUI.Button(new Rect(275,15,35,25),"Set"))
				CmdChangeColour(colourboxname);
		}
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (isLocalPlayer && collision.gameObject.tag =="bullet")
        {
            CmdChangeHealth(-5);
        }
    }

    //Credit for this method: from http://answers.unity3d.com/questions/812240/convert-hex-int-to-colorcolor32.html
    //hex for testing green: 04BF3404  red: 9F121204  blue: 221E9004
    Color ColorFromHex(string hex)
	{
		hex = hex.Replace ("0x", "");
        hex = hex.Replace ("#", "");
        byte a = 255;
        byte r = byte.Parse(hex.Substring(0,2), System.Globalization.NumberStyles.HexNumber);
        byte g = byte.Parse(hex.Substring(2,2), System.Globalization.NumberStyles.HexNumber);
        byte b = byte.Parse(hex.Substring(4,2), System.Globalization.NumberStyles.HexNumber);
        if(hex.Length == 8)
        {
             a = byte.Parse(hex.Substring(4,2), System.Globalization.NumberStyles.HexNumber);
        }
        return new Color32(r,g,b,a);
    }

	// Use this for initialization
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

        health = Instantiate(healthPrefab, Vector3.zero, Quaternion.identity) as Slider;
        health.transform.SetParent(canvas.transform);

        spawnPos = FindObjectsOfType<NetworkStartPosition>();
	}

    public override void OnStartClient()
    {
        base.OnStartClient();
        //Invoke("Init", 1);
    }

    public void OnDestroy()
	{
        if (nameLabel != null && health != null)
        {
            Destroy(nameLabel.gameObject);
            Destroy(health.gameObject);
        }
	}

	void Update()
	{
		//determine if the object is inside the camera's viewing volume
		if(nameLabel != null)
		{
			Vector3 screenPoint = Camera.main.WorldToViewportPoint(this.transform.position);
			bool onScreen = screenPoint.z > 0 && screenPoint.x > 0 && 
			                screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;
            //if it is on screen draw its label attached to is name position
            if (onScreen)
            {
                Vector3 nameLabelPos = Camera.main.WorldToScreenPoint(namePos.position);
                nameLabel.transform.position = nameLabelPos;
                health.transform.position = nameLabelPos + new Vector3(0, 20, 0);
            }
            else //otherwise draw it WAY off the screen 
            {
                nameLabel.transform.position = new Vector3(-1000, -1000, 0);
                health.transform.position = new Vector3(-1000, -1000, 0);
            }
        }
	}

    /*private void Init()
    {
        RpcOnChangeName(pName);
        RpcOnChangeColour(pColour);
    }*/
}
