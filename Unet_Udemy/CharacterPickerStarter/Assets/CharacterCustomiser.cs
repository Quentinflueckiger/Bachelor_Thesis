using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCustomiser : MonoBehaviour
{

    static CharacterCustomiser CC;
    static public GameObject myCharacter;
    public Texture[] JaneTops;
    public Texture[] BrutiusTops;
    public int currentTop = 0;

    public void ChangeTopTexture(int i)
    {
        if (myCharacter.name.Contains("Jane"))
            myCharacter.transform.Find("Tops").GetComponent<Renderer>().material.mainTexture = JaneTops[i];
        else if (myCharacter.name.Contains("Brutius"))
            myCharacter.transform.Find("Tops").GetComponent<Renderer>().material.mainTexture = BrutiusTops[i];

        myCharacter.GetComponent<SetupLocalPlayer>().CmdChangeTop(i);
        CC.currentTop = i;
    }

    static public Texture GetTop(int i, string name)
    {
        if (name.Contains("Jane"))
            return (CC.JaneTops[i]);
        else if (name.Contains("Brutius"))
            return (CC.BrutiusTops[i]);

        return null;
    }


    static public int GetTopId()
    {
        return CC.currentTop;
    }

    // Use this for initialization
    void Start()
    {
        CC = this;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
