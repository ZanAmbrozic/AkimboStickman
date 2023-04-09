using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;
    public NetOptions netOptions = new NetOptions();
    public string connType;
    private ulong _winner;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    public void SetPortNumber(ushort port)
    {
        netOptions.port = port;
    }

    public void SetIPAddress(string ip)
    {
        netOptions.ipAddress = ip;
    }

    public ulong Winner
    {
        get { return _winner; }
        set { _winner = value; }
    }
}
