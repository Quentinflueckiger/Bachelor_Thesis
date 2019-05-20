using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScene : MonoBehaviour
{

    public void SwitchToSnake()
    {
        SceneManager.LoadScene("SnakeLobby");
    }

    public void SwitchToDomino()
    {
        SceneManager.LoadScene("DominoLobby");
    }

    public void SwitchToHub()
    {
        SceneManager.LoadScene("Hub");
    }
}
