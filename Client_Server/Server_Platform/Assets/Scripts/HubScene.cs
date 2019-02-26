using UnityEngine;
using UnityEngine.SceneManagement;

public class HubScene : MonoBehaviour
{
    public static HubScene Instance { private set; get; }

    private void Start()
    {
        Instance = this;    
    }

    public void OnClickStartServer()
    {
        Server.Instance.Init();
    }

    public void OnClickStopServer()
    {
        Server.Instance.ShutDown();
    }

    public void GoToGame1Scene()
    {
        SceneManager.LoadScene("Game1");
    }
}
