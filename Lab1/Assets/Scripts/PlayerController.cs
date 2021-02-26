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
    Health m_playerHealth;
    bool m_hit = false;
    bool m_isDead = false;
    private float m_knockPower;
    private float m_timer;

    //jumping var
    private bool m_grounded;
    public float m_jumpPower;

    public LayerMask m_AttackLayer = 1 << 11 | 1 << 8;

    //projectile
    public GameObject m_projectile;
    public Transform m_shotSpawn;
    public float m_projectileSpeed;
    public float m_fireRate;
    private bool m_readyToFire;
    private float m_nextFire;
    private Vector2 m_dir;
    private Vector2 m_playerScale;

    //attack
    private float m_timeAttack;
    public float m_swingRate;
    public Transform m_attackPos;

    public float m_attackRange;
    public int m_damage;


    private Animator m_anim;

    void Start()
    {
        m_knockPower = 2.0f;
        m_anim = this.GetComponent<Animator>();
        m_rb = gameObject.GetComponent<Rigidbody2D>();
        m_playerHealth = GetComponent<Health>();
        m_timer = 5.0f;
        m_isDead = false;
        save();
    }

    // Update is called once per frame
    void Update()
    {
        if(!m_isDead)
        {
            movement();
            if (Input.GetKeyDown(KeyCode.Mouse1) && m_readyToFire)
            {
              
                Fire();
            }
            if (!m_readyToFire && Time.time > m_nextFire) // checks
            {
                m_readyToFire = true;
            }

            if (m_timeAttack <= 0)
            {
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    Attack();
                }
            }
            else
            {
                m_timeAttack -= Time.deltaTime;
            }
        }

        if (m_playerHealth.getHealth() == 0)
        {
            m_isDead = true;
            checkStatus();
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
        if (m_playerHealth.getMana() > 0)
        {
            GameObject m_projectilefired = Instantiate(m_projectile, m_shotSpawn.position, m_shotSpawn.rotation); // create
            m_nextFire = Time.time + m_fireRate;
            m_projectilefired.GetComponent<Rigidbody2D>().velocity = m_projectileSpeed * m_dir; // bullet is fired
            m_readyToFire = false;
            m_playerHealth.useMana();
        }
    }

    void Attack()
    {
        m_timeAttack = m_swingRate;
        var enemies = Physics2D.OverlapCircleAll(
           m_attackPos.position,
           m_attackRange,
           m_AttackLayer);
        m_anim.SetTrigger("Attack");

        foreach (Collider2D enemy in enemies)
        {
            if (enemy.CompareTag("Interact"))
                enemy.GetComponent<DestroyLight>().OnInteract();
            else if (enemy.CompareTag("Enemy"))
            {
                if (enemy.isTrigger)
                    continue;

                var enemyScript = enemy.GetComponent<AIBehavior>();
                if (enemyScript != null)
                {
                    enemyScript.TakeDamage(m_damage);
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            m_grounded = true; // player is on the ground
            m_anim.SetBool("IsJumping", false);
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            //m_hit = true;
            m_playerHealth.takeDamage(1);
            Debug.Log("You have collided");
        }

        if (collision.gameObject.CompareTag("Potion"))
        {
            m_playerHealth.heal(2);
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("Item"))
        {
            m_projectile.gameObject.GetComponent<SpriteRenderer>().sprite = collision.gameObject.GetComponent<SpriteRenderer>().sprite;
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("Money"))
        {
            Score.scoreValue += 50;
            Destroy(collision.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("FireBall"))
        {
            m_hit = true;
            Destroy(collision.gameObject);
            m_playerHealth.takeDamage(1);
            Debug.Log("You have collided with FireBall");
        }

        if (collision.CompareTag("Coffin"))
            save();
    }

    private void OnDrawGizmosSelected()
    {
        if (m_attackPos == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(m_attackPos.position, m_attackRange);
    }

    private void checkStatus()
    {
        m_rb.velocity = Vector2.zero;
        m_anim.SetBool("HasRespawned", false);
        m_anim.SetBool("IsDead", true);

        m_timer -= Time.deltaTime;
        if (m_timer <= 0)
        {
            m_anim.SetBool("IsDead", false);
            load();
            m_anim.SetBool("HasRespawned", true);
            m_playerHealth.heal(8);
            m_timer = 5f;
            m_isDead = false;
        }

    }

    public void save()
    {
        //Saving
        PlayerPrefs.SetFloat("PlayerX", this.transform.position.x);
        PlayerPrefs.SetFloat("PlayerY", this.transform.position.y);
        PlayerPrefs.SetFloat("PlayerZ", this.transform.position.z);
        Debug.Log("Saved");
    }

    private void load()
    {
        //Loading
        transform.position = new Vector3(PlayerPrefs.GetFloat("PlayerX"), PlayerPrefs.GetFloat("PlayerY"), PlayerPrefs.GetFloat("PlayerZ"));

    }
}




