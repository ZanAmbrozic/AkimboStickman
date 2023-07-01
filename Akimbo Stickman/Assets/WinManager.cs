using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinManager : MonoBehaviour
{
    private bool _inProgress;
    private bool _ended;

    public Transform p1Spawn;
    public Transform p2Spawn;

    private void Start()
    {
        _inProgress = false;
        _ended = false;
    }

    public void Update()
    {
        if (_inProgress == false && GameObject.FindGameObjectsWithTag("Player").Length >= 2)
        {
            //Resets player positions
            GameObject.FindGameObjectsWithTag("Player")[0].transform.position = p1Spawn.position;
            GameObject.FindGameObjectsWithTag("Player")[1].transform.position = p2Spawn.position;


            //Spawns obstacle
            string map = DataManager.instance.map;
            if (string.IsNullOrEmpty(map))
            {
                map = "Type1";
            }
            GameObject.Find("Obstacles").transform.Find(map).gameObject.SetActive(true);


            GameObject.FindGameObjectsWithTag("Player")[1].transform.Find("Canvas").Find("HealthBar").Find("Fill").gameObject.GetComponent<Image>().color = new Color(0.831f, 0.216f, 0.216f, 1);
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
