using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private int _bulletCount = 0;
    private float _timer;
    private float _burstTimer;

    private bool isGrounded = false;

    [HideInInspector] public bool facingRight = true;

    [HideInInspector] public List<int> doNotShoot;


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

            Instantiate(dropParticles, dropParticlesPoint.position, Quaternion.identity);

            isGrounded = true;
        }
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

        if (doNotShoot.Contains(hit.collider.transform.GetInstanceID()))
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
        firedBullet.GetComponent<BulletScript>().destroyWhenOutOfCamera = false;

        //Adds each parent to the list so it doesn't trigger collision
        firedBullet.GetComponent<BulletScript>().doNotHit.Add(transform.GetInstanceID());
    }
}
