using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class GloveScript : MonoBehaviour
{
    private Vector3 _mousePos;
    private Rigidbody2D _rb;
    private Camera _camera;
    private int _dmg;
    public float force;
    [HideInInspector] public bool targetMouse = false;

    [HideInInspector] public List<int> doNotHit;
    [HideInInspector] public ulong ownerID;

    private Vector3 _rotation;
    private Vector3 _direction;

    // Start is called before the first frame update
    void Start()
    {
        _dmg = 50;
        _camera = Camera.main;
        _rb = GetComponent<Rigidbody2D>();

        if (targetMouse)
        {
            //_mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);
            //Vector3 direction = _mousePos - transform.position;
            //Vector3 rotation = transform.position - _mousePos;
            _rb.velocity = new Vector2(_direction.x, _direction.y).normalized * force;
            float rot = Mathf.Atan2(_rotation.y, _rotation.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, 180 + rot);
        }
        else
        {
            _rb.velocity = transform.right * force;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    public void Init(Vector3 direction, Vector3 rotation)
    {
        _direction = direction;
        _rotation = rotation;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<NetworkObject>(out var colNetCmp))
        {
            Debug.Log("Owner: " + ownerID);
            if (colNetCmp.OwnerClientId == ownerID)
                return;
        }

        if (collision.TryGetComponent<HealthComponent>(out HealthComponent healthComponent))
        {
            if (healthComponent.DealDamage(_dmg) == false)
            {
                Destroy(collision.gameObject);
            }
        }

        Destroy(gameObject);
    }
}
