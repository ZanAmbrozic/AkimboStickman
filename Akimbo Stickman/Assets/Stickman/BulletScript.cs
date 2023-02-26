using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private Vector3 _mousePos;
    private Rigidbody2D _rb;
    private Camera _camera;
    public float force;
    public bool destroyWhenOutOfCamera = true;
    [HideInInspector] public bool targetMouse = false;

    [HideInInspector] public List<int> doNotHit;

    // Start is called before the first frame update
    void Start()
    {

        _camera = Camera.main;
        _rb = GetComponent<Rigidbody2D>();

        if (targetMouse)
        {
            _mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);
            Vector3 direction = _mousePos - transform.position;
            Vector3 rotation = transform.position - _mousePos;
            _rb.velocity = new Vector2(direction.x, direction.y).normalized * force;
            float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, rot + 90);
        }
        else
        {
            _rb.velocity = transform.right * force;
            transform.rotation = Quaternion.Euler(0, 0, 90);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (destroyWhenOutOfCamera)
            OutOfCamera();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!doNotHit.Contains(collision.gameObject.transform.GetInstanceID())) //Checks if it should pass through the object
        {   
            if (collision.gameObject.CompareTag("EnemyNPC"))
                Destroy(collision.gameObject);
            Destroy(gameObject);
        }

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
