using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;

public class PortValidator : MonoBehaviour
{
    public TMP_InputField inputField;

    public void ValidatePortNumber()
    {
        var text = inputField.text;
        ushort port;
        string pattern = @"^((6553[0-5])|(655[0-2][0-9])|(65[0-4][0-9]{2})|(6[0-4][0-9]{3})|([1-5][0-9]{4})|([0-5]{0,5})|([0-9]{1,4}))$";

        if (string.IsNullOrEmpty(text) || !Regex.IsMatch(text, pattern))
        {
            inputField.text = "1234";
            Debug.Log("NO Match");
        }
        else
        {
            Debug.Log("Match");
        }

        if (!ushort.TryParse(inputField.text, out port))
        {
            port = 1234;
        }
        DataManager.instance.SetPortNumber(port);
    }
}
