using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    // We start with the movement of the player
    public float m_speed;
    private float m_movement;
    private Rigidbody2D m_rb;
    // player health
    private int m_health;

    //jumping var
    private bool m_grounded;
    public float m_jumpPower;


    void Start()
    {
        m_rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        m_movement = Input.GetAxis("Horizontal");
        m_rb.velocity = new Vector2(m_movement * m_speed, m_rb.velocity.y);

        if (Input.GetKeyDown(KeyCode.Space) && m_grounded)
        {
            m_rb.AddForce(new Vector2(0, m_jumpPower), ForceMode2D.Impulse); 
            m_grounded = false; // player has jumped
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            m_grounded = true; // player is on the ground
            /*Anim.SetBool("IsJumping", false);*/
        }
    }



}
