using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playermove : MonoBehaviour
{
    public float maxSpeed;
    public float playerSpeedx;
    public float playerSpeedy;
    public int JumpPower;
    Rigidbody2D rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Move();
        Jump();
    }

    void Move()
    {
        float l = Input.GetAxisRaw("Horizontal");
        playerSpeedx = rigid.velocity.x;

        rigid.AddForce(Vector2.right * l, ForceMode2D.Impulse);
        if (rigid.velocity.x > maxSpeed)
        {
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
        }
        else if (rigid.velocity.x < maxSpeed * -1)
        {
            rigid.velocity = new Vector2(maxSpeed * -1, rigid.velocity.y);
        }
    }

    void Jump()
    {
        playerSpeedy = rigid.velocity.y;

        if (Input.GetButtonDown("Jump"))
        {
            rigid.AddForce(Vector2.up * JumpPower, ForceMode2D.Impulse);
        }
    }
}