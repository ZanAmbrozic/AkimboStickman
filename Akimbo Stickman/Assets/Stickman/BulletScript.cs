using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Unity.Netcode;

public class BulletScript : MonoBehaviour
{
    private Vector3 _mousePos;
    private Rigidbody2D _rb;
    private Camera _camera;
    public float force;
    public bool destroyWhenOutOfCamera = true;
    [HideInInspector] public bool targetMouse = false;

    [HideInInspector] public int dmg = 10;
    [HideInInspector] public List<int> doNotHit;
    [HideInInspector] public ulong ownerID = 10000;

    private Vector3 _rotation;
    private Vector3 _direction;


    // Start is called before the first frame update
    void Start()
    {

        _camera = Camera.main;
        _rb = GetComponent<Rigidbody2D>();

        if (targetMouse)
        {
            //_mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);
            //Vector3 direction = _mousePos - transform.position;
            ////Vector3 rotation = transform.position - _mousePos;
            _rb.velocity = new Vector2(_direction.x, _direction.y).normalized * force;
            float rot = Mathf.Atan2(_rotation.y, _rotation.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, rot + 90);
        }
        else
        {
            _rb.velocity = transform.right * force;
            transform.rotation = Quaternion.Euler(0, 0, 90);
        }
    }

    public void Init(Vector3 direction, Vector3 rotation)
    {
        _direction = direction;
        _rotation = rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (destroyWhenOutOfCamera)
            OutOfCamera();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent<NetworkObject>(out var colNetCmp))
        {
            Debug.Log("Owner: " + ownerID);
            if (colNetCmp.OwnerClientId == ownerID)
                return;
        }

        //else //Checks if it should pass through the object
        //{
            if (collision.TryGetComponent<HealthComponent>(out HealthComponent healthComponent))
            {
                if(healthComponent.DealDamage(dmg) == false)
                {
                    Destroy(collision.gameObject);
                }
            }
            Destroy(gameObject);
        //}

    }

    void OutOfCamera()
    {
        Vector3 viewPos = _camera.WorldToViewportPoint(GameObject.FindGameObjectWithTag("Bullet").transform.position);
        if (!(viewPos.x >= 0 && viewPos.x <= 1 && viewPos.y >= 0 && viewPos.y <= 1 && viewPos.z > 0))
        {
            GameObject.Destroy(GameObject.FindGameObjectWithTag("Bullet"));
        }
    }
}
