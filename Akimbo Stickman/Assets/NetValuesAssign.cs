using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;

public class NetValuesAssign : MonoBehaviour
{
    void Start()
    {
        string connType = DataManager.instance.connType;
        string ipAdress = DataManager.instance.netOptions.ipAdress;
        ushort port = DataManager.instance.netOptions.port;
        

        if(connType == "Host")
        {
            GameObject playerPrefab = GameManager.instance.currentCharacter.prefab;
            NetworkManager.Singleton.NetworkConfig.PlayerPrefab = playerPrefab;
            NetworkManager.Singleton.StartHost();
        }
        else
        {
            NetworkManager.Singleton.StartClient();
        }

        NetworkManager.Singleton.GetComponent<UnityTransport>().SetConnectionData(
            ipAdress,  // The IP address is a string
            port, // The port number is an unsigned short
            "0.0.0.0" // The server listen address is a string.
        );
    }

}
