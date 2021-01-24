using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float m_moveSpeed;
    private Rigidbody2D m_playerRGBD;
    public Vector2 m_movement;

    public float jumpVelocity;

    void Awake()
    {
        m_playerRGBD = this.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        m_movement = new Vector2(Input.GetAxis("Horizontal"), 0.0f);
       
       
    }

    private void FixedUpdate()
    {
        moveCharacter(m_movement);
        Jump();
    }

    void moveCharacter(Vector2 m_dir)
    {
        m_playerRGBD.velocity = m_dir * m_moveSpeed;
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            m_playerRGBD.AddRelativeForce(new Vector2(0,jumpVelocity), ForceMode2D.Impulse);

        }
    }
}