using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChampAbility : MonoBehaviour
{
    public bool isActive;
    public GameObject glove;
    public Transform shootingPoint;

    private PlayerAbilityManager _abilityManager;
    private BoxCollider2D _boxCollider;

    void Start()
    {
        _abilityManager = GetComponent<PlayerAbilityManager>();
        _boxCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Ability1") && _abilityManager.canActivate)
        {
            GloveSpawn();
        }
    }

    private void GloveSpawn()
    {
        _abilityManager.abilityActive = false;
        _abilityManager.canActivate = false;


        var gloveInstance = Instantiate(glove, shootingPoint.position, Quaternion.identity);
        var gloveScript = gloveInstance.GetComponent<GloveScript>();
        gloveScript.targetMouse = true;
        gloveScript.doNotHit.Add(transform.GetInstanceID());
        
    }
}
