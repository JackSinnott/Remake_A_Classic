using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float m_moveSpeed = 3.0f;
    private Rigidbody2D m_playerRGBD;
    public Vector2 m_movement;

    bool m_jumped = false;
    public float jumpVelocity = 2f;

    void Awake()
    {
        m_playerRGBD = this.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        m_movement = new Vector2(Input.GetAxis("Horizontal"), 0.0f);
        Jump();
       
    }

    private void FixedUpdate()
    {
        moveCharacter(m_movement);

    }

    void moveCharacter(Vector2 m_dir)
    {
        m_playerRGBD.velocity = m_dir * m_moveSpeed;
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (m_jumped == false)
            {
                m_jumped = true;
                Debug.Log("Jumping");
            }
        }
    }
}