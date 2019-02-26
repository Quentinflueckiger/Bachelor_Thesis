using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.Networking;

public class Server : MonoBehaviour
{
    public static Server Instance {private set; get; }

    private const int MAX_USER = 10;
    private const int PORT = 26000;
    private const int WEB_PORT = 26001;
    private const int BYTE_SIZE = 1024;

    private byte reliableChannel;
    private int hostId;
    private int webHostId;

    private bool isStarted;
    private byte error;

    #region MonoBehaviour
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        Instance = this;
        Init();
    }
    private void Update()
    {
        UpdateMessagePump();                
    }
    #endregion

    public void Init()
    {
        NetworkTransport.Init();

        ConnectionConfig cc = new ConnectionConfig();
        reliableChannel = cc.AddChannel(QosType.Reliable);

        HostTopology topo = new HostTopology(cc, MAX_USER);

        hostId = NetworkTransport.AddHost(topo, PORT, null);
        webHostId = NetworkTransport.AddWebsocketHost(topo, WEB_PORT, null);

        Debug.Log(string.Format("Opening connections on port {0} and web port {1}.", PORT, WEB_PORT));
        isStarted = true;
    }
    public void ShutDown()
    {
        isStarted = false;
        NetworkTransport.Shutdown();
    }

    // Check for message sent to the server
    public void UpdateMessagePump()
    {
        if (!isStarted)
            return;

        int recHostId;          // From which plateform is the message sent
        int connectionId;       // Which user is sending this
        int channelId;          // From which lane is the message sent

        byte[] recBuffer = new byte[BYTE_SIZE];
        int dataSize;

        NetworkEventType type = NetworkTransport.Receive(out recHostId, out connectionId, out channelId, recBuffer, BYTE_SIZE, out dataSize, out error);

        // Switch on the type of the message recieved
        switch (type)
        {

            case NetworkEventType.Nothing:
                break;

            case NetworkEventType.ConnectEvent:
                Debug.Log(string.Format("User {0} has connected through host {1}.", connectionId, recHostId));
                //HubScene.Instance.GoToGame1Scene();
                break;

            case NetworkEventType.DisconnectEvent:
                Debug.Log(string.Format("User {0} has disconnected.", connectionId));
                break;

            case NetworkEventType.DataEvent:
                BinaryFormatter formatter = new BinaryFormatter();
                MemoryStream ms = new MemoryStream(recBuffer);
                NetMsg msg = (NetMsg)formatter.Deserialize(ms);

                OnData(connectionId, channelId, recHostId, msg);
                break;

            default:
            case NetworkEventType.BroadcastEvent:
                Debug.Log("Unexpected network event type.");
                break;

        }
    }

    #region Send
    public void SendClient(int recHost, int connectionId, NetMsg msg)
    {
        byte[] buffer = new byte[BYTE_SIZE];

        // place to translate data to byte array
        BinaryFormatter formatter = new BinaryFormatter();
        MemoryStream ms = new MemoryStream(buffer);
        formatter.Serialize(ms, msg);

        if (recHost == 0)
            NetworkTransport.Send(hostId, connectionId, reliableChannel, buffer, BYTE_SIZE, out error);
        else
            NetworkTransport.Send(webHostId, connectionId, reliableChannel, buffer, BYTE_SIZE, out error);
    }
    #endregion

    #region OnData
    private void OnData(int connectionId, int channelId, int recHostId, NetMsg msg)
    {
        switch (msg.OP)
        {
            case NetOP.None:
                Debug.Log("Unexpected NetOP");
                break;

            case NetOP.LoginRequest:
                LoginRequest(connectionId, channelId, recHostId, (Net_LoginRequest)msg);
                break;
                
        }
    }
    private void LoginRequest(int connectionId, int channelId, int recHostId, Net_LoginRequest lr)
    {
        Debug.Log(string.Format("{0}", lr.Username));

        Net_OnLoginRequest olr = new Net_OnLoginRequest();
        olr.Success = 0;
        olr.Information = "Login succeeded";
        olr.Username = lr.Username;
        olr.Discriminator = "0001";
        olr.Token = "TOKEN";

        SendClient(recHostId, connectionId, olr);
    }
    #endregion

}
