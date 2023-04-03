using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using System;

public class TurretController : MonoBehaviour
{
    public GameObject bullet;
    public Transform firePoint;
    public bool canFire = true;
    public float fireRate;
    public bool canFireBurst;
    public float burstFireRate;

    public ParticleSystem dropParticles;
    public Transform dropParticlesPoint;

    public ParticleSystem destroyParticles;

    private int _bulletCount = 0;
    private float _timer;
    private float _burstTimer;

    private bool isGrounded = false;

    [HideInInspector] public GameObject creator;

    [HideInInspector] public bool facingRight = true;

    [HideInInspector] public List<ulong> doNotShoot;


    private void Start()
    {
        if (!facingRight)
        {
            transform.Rotate(0f, 180f, 0f);
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (!isGrounded && col.gameObject.CompareTag("ground"))
        {

            _ = Instantiate(dropParticles, dropParticlesPoint.position, Quaternion.identity);

            isGrounded = true;
        }
    }

    private void OnDestroy()
    {
        if (!this.gameObject.scene.isLoaded)
            return;

        _ = Instantiate(destroyParticles, transform.position, Quaternion.identity);
        creator.GetComponent<AkimboAbility>().isActive = false;

        Debug.Log("Turret owner: " + creator.GetComponent<NetworkObject>().OwnerClientId);
    }

    // Update is called once per frame
    void Update()
    {

        if (DetectEnemy())
        {
            FireRoutine();
        }
        
    }

    private bool DetectEnemy()
    {
        Vector2 direction = transform.right;

        RaycastHit2D hit = Physics2D.Raycast(firePoint.position, direction);

        if (hit.collider.gameObject == creator)
        {
            return false;
        }
        return true;
    }

    private void FireRoutine()
    {
        if (!canFireBurst)
        {
            _burstTimer += Time.deltaTime;
            if (_burstTimer > burstFireRate)
            {
                canFireBurst = true;
                _burstTimer = 0f;
            }
        }

        if (canFireBurst)
        {

            if (!canFire)
            {
                _timer += Time.deltaTime;
                if (_timer > fireRate)
                {
                    canFire = true;
                    _timer = 0f;
                }
            }

            if (canFire)
            {
                canFire = false;
                Fire();
                _bulletCount++;

            }
        }

        if (_bulletCount >= 3)
        {
            canFireBurst = false;
            _bulletCount = 0;
            canFire = true;
            _timer = 0f;
        }
    }

    private void Fire()
    {
        var firedBullet = Instantiate(bullet, firePoint.position, firePoint.rotation);

        var bulletScript = firedBullet.GetComponent<BulletScript>();
        bulletScript.destroyWhenOutOfCamera = false;
        bulletScript.dmg = 5;
        bulletScript.ownerID = 10000;

        //Adds each parent to the list so it doesn't trigger collision
        //bulletScript.doNotHit.Add(transform.GetInstanceID());
    }
}
