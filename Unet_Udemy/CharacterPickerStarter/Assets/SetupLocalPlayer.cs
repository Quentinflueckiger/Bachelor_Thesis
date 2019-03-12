using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

public class SetupLocalPlayer : NetworkBehaviour {

	Animator animator;

	[SyncVar (hook = "OnChangeAnimation")]
	public string animState = "idle";

    [SyncVar(hook = "OnChangeTop")]
    public int tid = 0;

	void OnChangeAnimation (string aS)
    {
		if(isLocalPlayer) return;
		UpdateAnimationState(aS);
    }

    [Command]
    public void CmdUpdatePlayerCharacter(int cid)
    {
        NetworkManager.singleton.GetComponent<CustomNetworkManager>().SwitchPlayer(this, cid);
    }

    [Command]
	public void CmdChangeAnimState(string aS)
	{
		UpdateAnimationState(aS);
	}

    [Command]
    public void CmdChangeTop(int t)
    {
        tid = t;
        this.transform.Find("Tops").GetComponent<Renderer>().material.mainTexture = CharacterCustomiser.GetTop(tid, this.name);
    }

    private void OnChangeTop(int t)
    {
        if (isLocalPlayer) return;
        tid = t;
        this.transform.Find("Tops").GetComponent<Renderer>().material.mainTexture = CharacterCustomiser.GetTop(tid, this.name);
    }

    void UpdateAnimationState(string aS)
	{
		if(animState == aS) return;
		animState = aS;
		if(animState == "idle")
			animator.SetBool("Idling",true);
		else if (animState == "run")
			animator.SetBool("Idling",false);
	}

	// Use this for initialization
	void Start () 
	{
		animator = GetComponentInChildren<Animator>();
        animator.SetBool("Idling", true);
        if (!isLocalPlayer)
            this.transform.Find("Tops").GetComponent<Renderer>().material.mainTexture = 
                CharacterCustomiser.GetTop(tid, this.name);

        if (isLocalPlayer)
		{
			GetComponent<PlayerController>().enabled = true;
			CameraFollow360.player = this.gameObject.transform;
            CharacterCustomiser.myCharacter = this.gameObject;
            this.transform.Find("Tops").GetComponent<Renderer>().material.mainTexture = 
                CharacterCustomiser.GetTop(CharacterCustomiser.GetTopId(), this.name);
            CmdChangeTop(CharacterCustomiser.GetTopId());
		}
		else
		{
			GetComponent<PlayerController>().enabled = false;
		}
	}

    private void OnGUI()
    {
        if (isLocalPlayer)
        {
            if (Event.current.Equals(Event.KeyboardEvent("0")) ||
                Event.current.Equals(Event.KeyboardEvent("1")) ||
                Event.current.Equals(Event.KeyboardEvent("2")) ||
                Event.current.Equals(Event.KeyboardEvent("3")))
            {
                int charID = int.Parse(Event.current.keyCode.ToString().Substring(5)) + 1;
                CmdUpdatePlayerCharacter(charID);
            }
        }
    }
}
