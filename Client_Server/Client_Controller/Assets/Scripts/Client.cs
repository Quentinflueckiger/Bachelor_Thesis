using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.Networking;

public class Client : MonoBehaviour
{

    public static Client Instance { private set; get; }

    private const int MAX_USER = 10;
    private const int PORT = 26000;
    private const int WEB_PORT = 26001;
    private const int BYTE_SIZE = 1024;
    //private const string SERVER_IP = "127.0.0.1";

    private byte reliableChannel;
    private int hostId;
    private int connectionId;
    private byte error;                 // Look at Networking.NetworkError in unity documentation

    public Account self;
    private bool isStarted;

    #region MonoBehaviour
    private void Start()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
        //Init();
    }
    private void Update()
    {
        UpdateMessagePump();
    }
    #endregion

    public void Init(string ServerIP)
    {
        NetworkTransport.Init();

        ConnectionConfig cc = new ConnectionConfig();
        reliableChannel = cc.AddChannel(QosType.Reliable);

        HostTopology topo = new HostTopology(cc, MAX_USER);

        // Client code
        hostId = NetworkTransport.AddHost(topo, 0);

        // Check if the client is running from the web and not in the editor to chose which port to connect to.
#if UNITY_WEBGL && !UNITY_EDITOR
        // Web client
        connectionId = NetworkTransport.Connect(hostId, ServerIP, WEB_PORT, 0, out error);
        Debug.Log("Connecting from web.");
#elif UNITY_ANDROID && !UNITY_EDITOR
        // Android
        Debug.Log("Connecting from Android.");
#elif UNITY_IOS && !UNITY_EDITOR
        // IOS
        Debug.Log("Connecting from IOS.");
#else
        // Standalone client
        connectionId = NetworkTransport.Connect(hostId, ServerIP, PORT, 0, out error);
        Debug.Log("Connecting from Standalone.");
#endif

        Debug.Log(string.Format("Attempting to connect on {0} ...", ServerIP));
        isStarted = true;
    }
    public bool ShutDown()
    {
        isStarted = false;
        NetworkTransport.Shutdown();
        return true;
    }

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

        switch (type)
        {

            case NetworkEventType.Nothing:
                break;

            case NetworkEventType.ConnectEvent:
                Debug.Log("We have connected to the server.");
                //ConnectionScene.Instance.GoToLobbyScene();
                break;

            case NetworkEventType.DisconnectEvent:
                Debug.Log("We have been disconnected from the server.");
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
    public void SendServer(NetMsg msg)
    {
        byte[] buffer = new byte[BYTE_SIZE];

        // place to translate data to byte array
        BinaryFormatter formatter = new BinaryFormatter();
        MemoryStream ms = new MemoryStream(buffer);
        formatter.Serialize(ms, msg);

        NetworkTransport.Send(hostId, connectionId, reliableChannel, buffer, BYTE_SIZE, out error);
    }
    public void SendLoginRequest(string username)
    {
        Net_LoginRequest lr = new Net_LoginRequest();

        lr.Username = username;

        SendServer(lr);
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

            case NetOP.OnLoginRequest:
                OnLoginRequest((Net_OnLoginRequest)msg);
                break;
        }
    }
    private void OnLoginRequest(Net_OnLoginRequest olr)
    {
        

        if (olr.Success != 0)
        {
            Debug.Log("Connection issue, unable to connect.");
        }
        else
        {
            // Successfull login
            Debug.Log(string.Format("{0}, {1}", olr.Information, olr.Username));

            // Saves the data about the client
            self = new Account();
            self.ActiveConnection = olr.ConnectionId;
            self.Username = olr.Username;
            self.Discriminator = olr.Discriminator;

            ConnectionScene.Instance.GoToLobbyScene();
        }
    }
    #endregion

    #region Tests

    #endregion
}
