using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;
    public NetOptions netOptions = new NetOptions();
    public string connType;

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

    public void SetIPAdress(string ip)
    {
        netOptions.ipAdress = ip;
    }
}
