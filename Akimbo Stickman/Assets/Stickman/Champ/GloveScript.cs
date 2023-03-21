using System.Collections;
using System.Collections.Generic;
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

    // Start is called before the first frame update
    void Start()
    {
        _dmg = 50;
        _camera = Camera.main;
        _rb = GetComponent<Rigidbody2D>();

        if (targetMouse)
        {
            _mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);
            Vector3 direction = _mousePos - transform.position;
            Vector3 rotation = transform.position - _mousePos;
            _rb.velocity = new Vector2(direction.x, direction.y).normalized * force;
            float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, 180 + rot);
        }
        else
        {
            _rb.velocity = transform.right * force;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!doNotHit.Contains(collision.gameObject.transform.GetInstanceID())) //Checks if it should pass through the object
        {
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
}
