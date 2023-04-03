using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class PlayerMove : NetworkBehaviour
{
    public int playerSpeed = 10;
    [HideInInspector] public bool facingRight;
    public int playerJumpPower = 1250;
    private float _moveX;
    public bool isGrounded;


    public override void OnNetworkSpawn()
    {
        if (!IsOwner)
        {
            enabled = false;
        }
    }

    private void Start()
    {
        facingRight = true;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMoveHandler();
    }

    void PlayerMoveHandler()
    {
        //CONTROLS
        _moveX = Input.GetAxis("Horizontal");
        if (Input.GetButtonDown("Jump") && isGrounded)
            Jump();

        //ANIMATIONS

        //PLAYER DIRECTION
        if (_moveX < 0.0f && facingRight == true)
            FlipPlayer();
        else if (_moveX > 0.0f && facingRight == false)
            FlipPlayer();

        //PHYSICS
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2 (_moveX * playerSpeed, gameObject.GetComponent<Rigidbody2D>().velocity.y);
    }

    void Jump()
    {
        GetComponent<Rigidbody2D>().AddForce(Vector2.up * playerJumpPower);
        isGrounded = false;
    }

    void FlipPlayer()
    {
        facingRight = !facingRight;

        transform.Rotate(0f, 180f, 0);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "ground")
        {
            isGrounded = true;
        }
    }
}
