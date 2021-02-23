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

    //projectile
    public GameObject m_projectile;
    public Transform m_shotSpawn;
    public float m_projectileSpeed;
    public float m_fireRate;
    private bool m_readyToFire;
    private float m_nextFire;
    private Vector2 m_dir;
    private Vector2 m_playerScale;

    private Animator m_anim;

    void Start()
    {
        m_anim = this.GetComponent<Animator>();
        m_rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        movement();
        if (Input.GetKeyDown(KeyCode.Mouse0) && m_readyToFire)
        {
            Fire();
        }
        if (!m_readyToFire && Time.time > m_nextFire) // checks
        {
            m_readyToFire = true;
        }
    }
    /// <summary>
    /// Player movement Function
    /// </summary>
    void movement()
    {
        m_movement = Input.GetAxis("Horizontal");
        m_rb.velocity = new Vector2(m_movement * m_speed, m_rb.velocity.y);
        m_anim.SetFloat("Speed", Mathf.Abs(m_movement));

        m_playerScale = transform.localScale;
        if (Input.GetAxis("Horizontal") < 0)
        {
            m_dir.x = -1.0f;
            m_playerScale.x = -5.0f;
        }
        if (Input.GetAxis("Horizontal") > 0)
        {
            m_dir.x = 1.0f;
            m_playerScale.x = 5.0f;
        }
        if (Input.GetKeyDown(KeyCode.Space) && m_grounded)
        {
            m_rb.AddForce(new Vector2(0, m_jumpPower), ForceMode2D.Impulse);
            m_grounded = false; // player has jumped
            m_anim.SetBool("IsJumping", true);
        }
        transform.localScale = m_playerScale;
        m_projectile.transform.localScale = m_playerScale/ 10;
    }

    /// <summary>
    /// Player projectile Fir
    /// </summary>
    void Fire()
    {
        GameObject m_projectilefired = Instantiate(m_projectile, m_shotSpawn.position, m_shotSpawn.rotation); // create
        m_nextFire = Time.time + m_fireRate;
        m_projectilefired.GetComponent<Rigidbody2D>().velocity = m_projectileSpeed * m_dir; // bullet is fired
        m_readyToFire = false;

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            m_grounded = true; // player is on the ground
            m_anim.SetBool("IsJumping", false);
        }
    }
}
