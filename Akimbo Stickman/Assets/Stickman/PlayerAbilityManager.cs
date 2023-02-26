using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Set abilityActive and canActivate properties in scripts that incorporate this one
/// </summary>
public class PlayerAbilityManager : MonoBehaviour
{
    public bool abilityActive;

    public float timer;
    public bool canActivate;
    public float cooldown;

    //TODO: Remove after testing
    //private float _timerTmp = 0f;

    private void Start()
    {
        timer = 0f;
        abilityActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!abilityActive && !canActivate)
        {
            timer += Time.deltaTime;
            if (timer > cooldown)
            {
                canActivate = true;
                timer = 0f;
            }
        }

        //TODO: Remove after testing
        //if (abilityActive) 
        //{
        //    _timerTmp += Time.deltaTime;
        //    if (_timerTmp > 3)
        //    {
        //        abilityActive = false;
        //        _timerTmp = 0f;
        //    }
        //}
    }
}
