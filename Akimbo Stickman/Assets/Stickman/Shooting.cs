using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.Netcode;
using UnityEngine;

public class Shooting : NetworkBehaviour
{
    private Vector3 _mousePos;
    public GameObject bullet;
    public Transform bulletTransform;
    public bool canFire;
    private float _timer;
    public float fireRate;
    private Camera _camera;


    private void Start()
    {
        _camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsOwner)
        {
            return;
        }

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

            _mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);
            Vector3 direction = _mousePos - transform.position;
            Vector3 rotation = transform.position - _mousePos;


            RequestFireServerRpc(direction, rotation);

        }
    }

    [ServerRpc]
    private void RequestFireServerRpc(Vector3 dir, Vector3 rot)
    {
        FireClientRpc(dir, rot);
    }

    [ClientRpc]
    private void FireClientRpc(Vector3 dir, Vector3 rot)
    {
        ExecuteShoot(dir, rot);
    }
    
    private void ExecuteShoot(Vector3 dir, Vector3 rot)
    {

        var firedBullet = Instantiate(bullet, bulletTransform.position, Quaternion.identity);


        firedBullet.GetComponent<BulletScript>().targetMouse = true;
        firedBullet.GetComponent<BulletScript>().dmg = 10;
        firedBullet.GetComponent<BulletScript>().destroyWhenOutOfCamera = false;

        firedBullet.GetComponent<BulletScript>().Init(dir, rot);
        firedBullet.GetComponent<BulletScript>().ownerID = OwnerClientId;

        //Adds each parent to the list so it doesn't trigger collision
        //firedBullet.GetComponent<BulletScript>().doNotHit.Add(transform.GetInstanceID());
        //firedBullet.GetComponent<BulletScript>().doNotHit.Add(transform.parent.GetInstanceID());
        //firedBullet.GetComponent<BulletScript>().doNotHit.Add(transform.parent.parent.GetInstanceID());
        //firedBullet.GetComponent<BulletScript>().doNotHit.Add(transform.parent.parent.parent.GetInstanceID());
    }
}
