public static class NetOP
{
    public const int None = 0;

    public const int LoginRequest = 1;
    public const int OnLoginRequest = 2;
}

[System.Serializable]
public abstract class NetMsg
{
    // Operation Code
    public byte OP { set; get; }

    public NetMsg()
    {
        OP = NetOP.None;
    }
}
