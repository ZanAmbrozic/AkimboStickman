using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class ChampAbility : NetworkBehaviour
{
    public bool isActive;
    public GameObject glove;
    public Transform shootingPoint;

    private Vector3 _mousePos;
    private Camera _camera;
    private PlayerAbilityManager _abilityManager;
    private BoxCollider2D _boxCollider;

    void Start()
    {
        _camera = Camera.main;
        _abilityManager = GetComponent<PlayerAbilityManager>();
        _boxCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        if (!IsOwner)
        {
            return;
        }

        if (Input.GetButtonDown("Ability1") && _abilityManager.canActivate)
        {
            _mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);
            Vector3 direction = _mousePos - transform.position;
            Vector3 rotation = transform.position - _mousePos;

            RequestSpawnServerRpc(direction, rotation);
        }
    }

    [ServerRpc]
    private void RequestSpawnServerRpc(Vector3 dir, Vector3 rot)
    {
        SpawnClientRpc(dir, rot);
    }

    [ClientRpc]
    private void SpawnClientRpc(Vector3 dir, Vector3 rot)
    {
        GloveSpawn(dir, rot);
    }

    private void GloveSpawn(Vector3 dir, Vector3 rot)
    {
        _abilityManager.abilityActive = false;
        _abilityManager.canActivate = false;


        var gloveInstance = Instantiate(glove, shootingPoint.position, Quaternion.identity);
        var gloveScript = gloveInstance.GetComponent<GloveScript>();
        gloveScript.targetMouse = true;
        gloveScript.Init(dir, rot);
        gloveScript.ownerID = OwnerClientId;
        
    }
}
