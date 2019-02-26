using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConnectionScene : MonoBehaviour
{
    public static ConnectionScene Instance { private set; get; }

    public InputField IPInput;
    public InputField UsernameInput;

    private void Start()
    {
        Instance = this;
    }

    public void OnClickConnectTo()
    {
        //Client.Instance.Init("127.0.0.1");
        Client.Instance.Init(IPInput.text);
    }
    public void OnClickDisconnect()
    {
        if (Client.Instance.ShutDown())
            Debug.Log("Disconnected.");

    }

    public void GoToLobbyScene()
    {
        SceneManager.LoadScene("Lobby");
    }

    #region Tests
    public void SendLogin()
    {
        Client.Instance.SendLoginRequest(UsernameInput.text);
    }
    #endregion
}
