using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBehavior : MonoBehaviour
{
    Rigidbody2D m_rb;
    private ItemDrop m_getItem;

    //health
    public int m_health;
    public Animator m_anim;
    private float m_deathTimer;

    //AI simple movement
    private bool m_patrol;
    private bool m_changePath;
    public float m_moveSpeed;

    //checks for collisions
    public Transform m_groundPos;
    public LayerMask m_groundLayer;
    public LayerMask m_wallLayer;
    public Collider2D m_bodyCollider;


    void Start()
    {
        m_patrol = true;
        m_changePath = false;
        m_rb = GetComponent<Rigidbody2D>();
        m_getItem = GetComponent<ItemDrop>();
        m_deathTimer = 0.75f;
    }

    // Update is called once per frame
    void Update()
    {

        if (m_patrol)
        {
            Patrol();
        }

        if (m_health <= 0)
        {
            Die();
        }
    }

    private void FixedUpdate()
    {
        if(m_patrol)
        {
            m_changePath = !Physics2D.OverlapCircle(m_groundPos.position, 0.1f, m_groundLayer);
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Projectile"))
        {
            if (m_getItem != null)
            {
                m_getItem.DropItem();
                Debug.Log("Dropped an Item " + m_getItem);
            }
        }

    }

    public void TakeDamage(int damage)
    {
        m_health -= damage;
        Debug.Log("Damage Taken");
    }

    void Die()
    {
        m_anim.SetBool("IsDead", true);
        Debug.Log("EnemyDied");

        GetComponent<Collider2D>().enabled = false;

        m_deathTimer -= Time.deltaTime;


        if (m_deathTimer <= 0f)
        {
            Destroy(this.gameObject);
            Debug.Log(m_deathTimer);
            m_deathTimer = 1f;
        }
    }

    void Patrol()
    {
        if(m_changePath || m_bodyCollider.IsTouchingLayers(m_groundLayer) || m_bodyCollider.IsTouchingLayers(m_wallLayer))
        {
            changeDirection();
        }

        m_rb.velocity = new Vector2(m_moveSpeed * Time.fixedDeltaTime, m_rb.velocity.y);
    }

    public void changeDirection()
    {
        m_patrol = false;
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        m_moveSpeed *= -1;
        m_patrol = true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(m_groundPos.position, 0.1f);
    }

    public bool getStatus()
    {
        return m_patrol;
    }
    public void setStatus(bool t_status)
    {
      m_patrol = t_status;
    }

    public void setVelocity(Vector2 t_velocity)
    {
        m_rb.velocity = t_velocity;
    }
}
