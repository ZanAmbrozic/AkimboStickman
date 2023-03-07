using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ChampAttack : MonoBehaviour
{
    public bool canFire;
    public float fireRate;
    public GameObject attackArea;

    private Vector3 _mousePos;
    private Camera _camera;
    private float _timer;
    private Vector2 _playerPos;
    private Vector2 _attackSize;

    private void Start()
    {
        _attackSize = attackArea.GetComponent<Renderer>().bounds.size;
        _camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
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

        if (Input.GetMouseButtonDown(0) && canFire)
        {
            canFire = false;

            _mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);
            Vector3 direction = _mousePos - transform.position;
            Vector3 rotation = transform.position - _mousePos;
            float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

            
            _playerPos = transform.position;
            RaycastHit2D[] hit = Physics2D.BoxCastAll(_playerPos, _attackSize, rot, direction, _attackSize.x);
            _ = BoxCast(_playerPos, _attackSize, rot, direction, _attackSize.x);


            Vector2 spawnPos =  new Vector2(-(_attackSize.x), 0);
            Quaternion q = Quaternion.AngleAxis(rot, new Vector3(0, 0, 1));
            spawnPos = q * spawnPos;
            spawnPos += _playerPos;
            _ = Instantiate(attackArea, spawnPos, Quaternion.Euler(0, 0, rot));
        }
    }

    static public RaycastHit2D BoxCast(Vector2 origen, Vector2 size, float angle, Vector2 direction, float distance) //Makes a boxcast visible
    {
        RaycastHit2D hit = Physics2D.BoxCast(origen, size, angle, direction, distance);

        //Setting up the points to draw the cast
        Vector2 p1, p2, p3, p4, p5, p6, p7, p8;
        float w = size.x * 0.5f;
        float h = size.y * 0.5f;
        p1 = new Vector2(-w, h);
        p2 = new Vector2(w, h);
        p3 = new Vector2(w, -h);
        p4 = new Vector2(-w, -h);

        Quaternion q = Quaternion.AngleAxis(angle, new Vector3(0, 0, 1));
        p1 = q * p1;
        p2 = q * p2;
        p3 = q * p3;
        p4 = q * p4;

        p1 += origen;
        p2 += origen;
        p3 += origen;
        p4 += origen;

        Vector2 realDistance = direction.normalized * distance;
        p5 = p1 + realDistance;
        p6 = p2 + realDistance;
        p7 = p3 + realDistance;
        p8 = p4 + realDistance;

        //Drawing the cast
        Color castColor = hit ? Color.red : Color.green;
        Debug.DrawLine(p1, p2, castColor, 1f);
        Debug.DrawLine(p2, p3, castColor, 1f);
        Debug.DrawLine(p3, p4, castColor, 1f);
        Debug.DrawLine(p4, p1, castColor, 1f);

        Debug.DrawLine(p5, p6, castColor, 1f);
        Debug.DrawLine(p6, p7, castColor, 1f);
        Debug.DrawLine(p7, p8, castColor, 1f);
        Debug.DrawLine(p8, p5, castColor, 1f);

        Debug.DrawLine(p1, p5, Color.grey, 1f);
        Debug.DrawLine(p2, p6, Color.grey, 1f);
        Debug.DrawLine(p3, p7, Color.grey, 1f);
        Debug.DrawLine(p4, p8, Color.grey, 1f);

        if (hit)
        {
            Debug.DrawLine(hit.point, hit.point + hit.normal.normalized * 0.2f, Color.yellow);
        }

        return hit;
    }
}
