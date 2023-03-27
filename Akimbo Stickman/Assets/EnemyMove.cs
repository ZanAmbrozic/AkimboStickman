using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public int enemySpeed;
    public int xMoveDirection;

    private void Start()
    {
        xMoveDirection = 1;
        enemySpeed = 5;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D[] hit = Physics2D.RaycastAll(transform.position, new Vector2(xMoveDirection, 0));
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(xMoveDirection, 0) * enemySpeed;
        if (hit.Length >= 1)
        {
            if (hit[1].distance < 0.7f)
            {
                Flip();
            }
        }
    }
    void Flip()
    {
        xMoveDirection *= -1;
    }
}
