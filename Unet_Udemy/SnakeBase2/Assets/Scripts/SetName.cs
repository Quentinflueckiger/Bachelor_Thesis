using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetName : MonoBehaviour
{
    private InputField input;
    private string playerName;

    void Start()
    {
        input = GetComponent<InputField>();

        playerName = PlayerPrefs.GetString("Name");

        if (playerName == null)
        {
            playerName = "NoName";
        }

        input.text = playerName;
    }

    public void Submit()
    {
        PlayerPrefs.SetString("Name", input.text);
    }
}
