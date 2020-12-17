using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float m_moveSpeed = 3.0f;
    private Rigidbody2D m_playerRGBD;
    private Animator m_playerAnim;
    public Vector2 m_movement;
    private bool m_facingRight = true;
    private Vector3 m_localScale;

    public bool isGrounded = true;

    void Awake()
    {
        m_playerRGBD = this.GetComponent<Rigidbody2D>();
        m_playerAnim = this.GetComponent<Animator>();
        m_localScale = transform.localScale;
    }

    private void Update()
    {
        Jump();
        m_movement = new Vector2(Input.GetAxis("Horizontal"), 0.0f);

        if (m_movement.x != 0f)
        {
            m_playerAnim.SetBool("isRunning", true);
        }
        else
            m_playerAnim.SetBool("isRunning", false);
        
        if(m_playerRGBD.velocity.y == 0)
        {
            m_playerAnim.SetBool("isJumping", false);
            m_playerAnim.SetBool("isFalling", false);
        }

        if(m_playerRGBD.velocity.y > 0)
        {
            m_playerAnim.SetBool("isJumping", true);
        }

        if (m_playerRGBD.velocity.y < 0)
        {
            m_playerAnim.SetBool("isJumping", false);
            m_playerAnim.SetBool("isFalling", true);
        }
    }

    private void FixedUpdate()
    {
        moveCharacter(m_movement);
    }

    private void LateUpdate()
    {
        if (m_movement.x > 0)
        {
            m_facingRight = true;
        }
        else if (m_movement.x < 0)
        {
            m_facingRight = false;
        }

        if (((m_facingRight) && (m_localScale.x < 0)) || ((!m_facingRight) && (m_localScale.x > 0)))
        {
            m_localScale.x *= -1;
        }

        transform.localScale = m_localScale;
    }
    void moveCharacter(Vector2 m_dir)
    {
        m_playerRGBD.velocity = m_dir * m_moveSpeed;
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            
        }
    }
}