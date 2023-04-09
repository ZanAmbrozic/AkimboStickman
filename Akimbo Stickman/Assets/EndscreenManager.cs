using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Unity.Netcode;

public class EndscreenManager : MonoBehaviour
{
    public TMP_Text winnerText;

    private void Awake()
    {

        ulong winner = DataManager.instance.Winner;

        Debug.Log(DataManager.instance.Winner);
        string txt = winner switch
        {
            0 => "Host",
            _ => "Client " + winner
        };

        winnerText.text = txt + " " + winnerText.text;
    }
}
