using System;
using UnityEngine;
using TMPro;
using System.Text.RegularExpressions;

public class IPValidator : MonoBehaviour
{

    public TMP_InputField inputField;

    public void ValidateIP()
    {
        var text = inputField.text;
        string ip;
        string pattern = @"(\b25[0-5]|\b2[0-4][0-9]|\b[01]?[0-9][0-9]?)(\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)){3}";

        if (string.IsNullOrEmpty(text) || !Regex.IsMatch(text, pattern))
        {
            inputField.text = "127.0.0.1";
            Debug.Log("NO Match");
        }
        else
        {
            Debug.Log("Match");
        }
        ip = inputField.text;

        DataManager.instance.SetIPAddress(ip);
    }
}
