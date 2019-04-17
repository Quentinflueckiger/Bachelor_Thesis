using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuHandler : MonoBehaviour
{
    public GameObject pauseMenu;
    private bool status = false;

    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            status = !status;
            pauseMenu.SetActive(status);
            if (status)
            {
                PauseGame();
            }
            else
            {
                ContinueGame();
            }
        }
    }

    private void PauseGame()
    {
        Time.timeScale = 0;
        // TODO : Stop players
    }

    private void ContinueGame()
    {
        Time.timeScale = 1;
        // TODO : Rehenable players
    }

    #region Buttons
    public void OpenSettings()
    {   
        // TODO : Implement settings
        Debug.Log("Open Settings, not implemented yet.");
    }

    public void GoBackToHub()
    {
        // TODO : Implementation
        Debug.Log("Back to hub, not implemented yet.");
    }

    public void QuitApplication()
    {
        Debug.Log("Game quited");
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
    #endregion
}
