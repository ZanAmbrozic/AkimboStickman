using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinManager : MonoBehaviour
{
    private bool _inProgress;
    private bool _ended;

    private void Start()
    {
        _inProgress = false;
        _ended = false;
    }

    public void Update()
    {
        if (_inProgress == false && GameObject.FindGameObjectsWithTag("Player").Length >= 2)
        {
            _inProgress = true;
        }

        if (_ended == false && _inProgress == true && GameObject.FindGameObjectsWithTag("Player").Length <= 1)
        {
            _ended = true;
            DataManager.instance.Winner = GameObject.FindGameObjectWithTag("Player").GetComponent<NetworkObject>().OwnerClientId;
            Disconnect();

            
            //_ = NetworkManager.Singleton.SceneManager.LoadScene("Endscreen", LoadSceneMode.Single);
        }
    }

    private void Disconnect()
    {
        if (NetworkManager.Singleton.IsHost)
        {
            StartCoroutine(ExecuteAfterTime(0.5f));
            return;
        }

        NetworkManager.Singleton.Shutdown();
        Destroy(GameObject.Find("NetworkManager"));
        SceneManager.LoadScene("Endscreen");


    }

    private IEnumerator ExecuteAfterTime(float time)
    {
        yield return new WaitForSeconds(time);

        // Code to execute after the delay
        NetworkManager.Singleton.Shutdown();
        Destroy(GameObject.Find("NetworkManager"));
        SceneManager.LoadScene("Endscreen");
    }
}
