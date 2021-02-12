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


    void Start()
    {
        m_rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        m_movement = Input.GetAxis("Horizontal");
        m_rb.velocity = new Vector2(m_movement * m_speed, m_rb.velocity.y);
    }
}
