using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    private Vector3 _mousePos;
    public GameObject bullet;
    public Transform bulletTransform;
    public bool canFire;
    private float _timer;
    public float fireRate;

    // Update is called once per frame
    void Update()
    {  
        if (!canFire)
        {
            _timer += Time.deltaTime;
            if(_timer > fireRate)
            {
                canFire = true;
                _timer = 0f;
            }
        }

        if (Input.GetMouseButtonDown(0) && canFire)
        {
            canFire = false;
            var firedBullet = Instantiate(bullet, bulletTransform.position, Quaternion.identity);

            firedBullet.GetComponent<BulletScript>().targetMouse = true;

            //Adds each parent to the list so it doesn't trigger collision
            firedBullet.GetComponent<BulletScript>().doNotHit.Add(transform.GetInstanceID());
            firedBullet.GetComponent<BulletScript>().doNotHit.Add(transform.parent.GetInstanceID());
            firedBullet.GetComponent<BulletScript>().doNotHit.Add(transform.parent.parent.GetInstanceID());
            firedBullet.GetComponent<BulletScript>().doNotHit.Add(transform.parent.parent.parent.GetInstanceID());

        }
    }
}
