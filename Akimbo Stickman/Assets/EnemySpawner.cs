using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public GameObject enemy;

    // Update is called once per frame
    void Update()
    {
        if(GameObject.FindGameObjectsWithTag("EnemyNPC").Length == 0)
        {
            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        Instantiate(enemy);
    }
}
