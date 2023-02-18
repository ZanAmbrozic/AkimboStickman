using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAblityManager : MonoBehaviour
{
    private bool _abilityActive = false;

    private float _timer = 0f;
    public bool canActivate;
    public float cooldown;

    private float _timerTmp = 0f; //TODO: Remove after testing

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Ability1") && canActivate)
        {
            Debug.Log("Ability");
            _abilityActive = true;
            canActivate = false;
        }

        if (!_abilityActive && !canActivate)
        {
            _timer += Time.deltaTime;
            if (_timer > cooldown)
            {
                canActivate = true;
                _timer = 0f;
            }
        }

        if (_abilityActive) //TODO: Remove after testing
        {
            _timerTmp += Time.deltaTime;
            if (_timerTmp > 3)
            {
                _abilityActive = false;
                _timerTmp = 0f;
            }
        }
    }
}
