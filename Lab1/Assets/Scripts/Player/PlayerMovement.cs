using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float m_moveSpeed;
    private float moveAmountHorizontal;
    public float m_movement;
    bool m_facingRight = true;

    private Animator Anim;
    private Rigidbody2D playerRGBD;
    private SpriteRenderer spriteRend;

    private void Awake()
    {
        spriteRend = this.GetComponent<SpriteRenderer>();
        playerRGBD = this.GetComponent<Rigidbody2D>();
        Anim = this.GetComponent<Animator>();
    }

    private void Update()
    {
        m_movement = Input.GetAxis("Horizontal");

        Anim.SetFloat("Speed", Mathf.Abs(m_movement));

        if (m_movement < 0f)
        {
            m_facingRight = false;
        }
        else
        {
            m_facingRight = true;
        }

        if (m_facingRight)
        {
            spriteRend.flipX = false;

        }
        else
        {
            spriteRend.flipX = true;
        }
    }

    private void FixedUpdate()
    {
        moveCharacter(m_movement);

        playerRGBD.velocity = new Vector2(moveAmountHorizontal, playerRGBD.velocity.y);

    }
    void moveCharacter(float m_dir)
    {
        moveAmountHorizontal = m_dir * m_moveSpeed;
    }
}
