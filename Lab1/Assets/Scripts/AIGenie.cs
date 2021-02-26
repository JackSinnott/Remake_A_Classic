using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIGenie : MonoBehaviour
{
    private float m_targetDistance;
    public float m_range, m_fireRate, m_bulletSpeed;
    public Transform m_target;
    public Transform m_shotSpawn;
    private AIBehavior m_AIBehavior;
    public GameObject m_projectile;
    bool m_readyToFire;
    private Vector2 m_dir;

    // Start is called before the first frame update
    private void Start()
    {
        m_AIBehavior = GetComponent<AIBehavior>();
        m_dir.x = -1.0f;
        m_readyToFire = false;
    }

    // Update is called once per frame
    void Update()
    {
        m_targetDistance = Vector2.Distance(transform.position, m_target.position);

        if (m_targetDistance <= m_range)
        {
            if (m_target.position.x > transform.position.x && transform.localScale.x > 0
                || m_target.position.x < transform.position.x && transform.localScale.x < 0)
            {
                m_AIBehavior.changeDirection();
                m_dir.x *= -1;
            }
            if (!m_readyToFire && Time.time > m_fireRate) // checks
            {
                m_readyToFire = true;
            }
            m_AIBehavior.setStatus(false);
            m_AIBehavior.setVelocity(Vector2.zero);
        }
        else
        {
            m_AIBehavior.setStatus(true);
        }

        if (m_readyToFire)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject m_projectilefired = Instantiate(m_projectile, m_shotSpawn.position, m_shotSpawn.rotation); // create
        m_fireRate = Time.time + m_fireRate;
        m_projectilefired.GetComponent<Rigidbody2D>().velocity = m_bulletSpeed * m_dir; // bullet is fired
        m_readyToFire = false;
    }
}


