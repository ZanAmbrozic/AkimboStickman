using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    public GameObject bullet;
    public Transform bulletTransform;
    public bool canFire = true;
    public float fireRate;
    public bool canFireBurst;
    public float burstFireRate;

    private int _bulletCount = 0;
    private float _timer;
    private float _burstTimer;

    // Update is called once per frame
    void Update()
    {
        FireRoutine();
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
        var firedBullet = Instantiate(bullet, bulletTransform.position, Quaternion.identity);
        firedBullet.GetComponent<BulletScript>().destroyWhenOutOfCamera = false;

        //Adds each parent to the list so it doesn't trigger collision
        firedBullet.GetComponent<BulletScript>().doNotHit.Add(transform.GetInstanceID());
    }
}
