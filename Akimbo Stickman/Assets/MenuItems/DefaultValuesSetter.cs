using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DefaultValuesSetter : MonoBehaviour
{
    public TMP_InputField ipInputField;
    public TMP_InputField portInputField;

    // Start is called before the first frame update
    void Start()
    {
        string ip = DataManager.instance.netOptions.ipAddress;
        ushort port = DataManager.instance.netOptions.port;

        if (!string.IsNullOrEmpty(ip))
        {
            ipInputField.text = ip;
        }
        if (port != 0)
        {
            portInputField.text = port.ToString();
        }
    }
}
