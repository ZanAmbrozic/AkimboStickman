using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AkimboAbility : MonoBehaviour
{
    public bool isActive;
    public GameObject turret;
    public Vector2 playerPos;

    private Vector2 _turretSize;
    private PlayerAbilityManager _abilityManager;
    private BoxCollider2D _boxCollider;

    void Start()
    {
        _abilityManager = GetComponent<PlayerAbilityManager>();
        _turretSize = turret.GetComponent<BoxCollider2D>().size * turret.transform.localScale;
        _boxCollider = GetComponent<BoxCollider2D>();
        isActive = false;
    }

    void Update()
    {
        //Vector2 direction = facingRight ? transform.right : -transform.right;

        if (Input.GetButtonDown("Ability1") && _abilityManager.canActivate)
        {
            Debug.Log("Ability");
            TurretSpawn();
        }

        if (isActive != _abilityManager.abilityActive)
        {
            _abilityManager.abilityActive = isActive;
        }
    }

    private void TurretSpawn()
    {
        playerPos = _boxCollider.transform.position;
        RaycastHit2D[] hit = Physics2D.BoxCastAll(playerPos, _turretSize, 0f, transform.right, 3 * _turretSize.y);
        _ = BoxCast(playerPos, _turretSize, 0f, transform.right, 3 * _turretSize.y);

        if (hit.Length <= 1)
        {
            bool playerFacingRight = GetComponent<PlayerMove>().facingRight;

            Vector2 spawnPos = playerFacingRight switch         //Location is the same as in the end of raycast
            {
                true => new Vector2(playerPos.x + (3 * _turretSize.y), playerPos.y),
                false => new Vector2(playerPos.x - (3 * _turretSize.y), playerPos.y)
            };


            var turretInstance = Instantiate(turret, spawnPos, Quaternion.identity);

            var turretController = turretInstance.GetComponent<TurretController>();
            turretController.doNotShoot.Add(transform.GetInstanceID());
            turretController.facingRight = playerFacingRight;
            turretController.creator = this.gameObject;

            _abilityManager.abilityActive = true;
            isActive = true;
            _abilityManager.canActivate = false;
        }
    }


    static public RaycastHit2D BoxCast(Vector2 origen, Vector2 size, float angle, Vector2 direction, float distance) //Makes a boxcast visible
    {
        RaycastHit2D hit = Physics2D.BoxCast(origen, size, angle, direction, distance);

        //Setting up the points to draw the cast
        Vector2 p1, p2, p3, p4, p5, p6, p7, p8;
        float w = size.x * 0.5f;
        float h = size.y * 0.5f;
        p1 = new Vector2(-w, h);
        p2 = new Vector2(w, h);
        p3 = new Vector2(w, -h);
        p4 = new Vector2(-w, -h);

        Quaternion q = Quaternion.AngleAxis(angle, new Vector3(0, 0, 1));
        p1 = q * p1;
        p2 = q * p2;
        p3 = q * p3;
        p4 = q * p4;

        p1 += origen;
        p2 += origen;
        p3 += origen;
        p4 += origen;

        Vector2 realDistance = direction.normalized * distance;
        p5 = p1 + realDistance;
        p6 = p2 + realDistance;
        p7 = p3 + realDistance;
        p8 = p4 + realDistance;


        //Drawing the cast
        Color castColor = hit ? Color.red : Color.green;
        Debug.DrawLine(p1, p2, castColor, 1f);
        Debug.DrawLine(p2, p3, castColor, 1f);
        Debug.DrawLine(p3, p4, castColor, 1f);
        Debug.DrawLine(p4, p1, castColor, 1f);

        Debug.DrawLine(p5, p6, castColor, 1f);
        Debug.DrawLine(p6, p7, castColor, 1f);
        Debug.DrawLine(p7, p8, castColor, 1f);
        Debug.DrawLine(p8, p5, castColor, 1f);

        Debug.DrawLine(p1, p5, Color.grey, 1f);
        Debug.DrawLine(p2, p6, Color.grey, 1f);
        Debug.DrawLine(p3, p7, Color.grey, 1f);
        Debug.DrawLine(p4, p8, Color.grey, 1f);
        if (hit)
        {
            Debug.DrawLine(hit.point, hit.point + hit.normal.normalized * 0.2f, Color.yellow);
        }

        return hit;
    }
}
