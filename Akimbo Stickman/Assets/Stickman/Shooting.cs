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

        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(bullet, bulletTransform.position, Quaternion.identity);
        }
    }
}
