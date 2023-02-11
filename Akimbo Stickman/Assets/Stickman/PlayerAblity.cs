using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAblity : MonoBehaviour
{
    private bool _cloneActive = false;
    public PlayerHealth playerHealth;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Ability1") && !_cloneActive && false) //TODO: Remove the last part
        {
            _cloneActive = true;
            CreateClone();

        }
    }

    void CreateClone()
    {
        playerHealth.health = Mathf.CeilToInt(playerHealth.health / 2);
    }
}
