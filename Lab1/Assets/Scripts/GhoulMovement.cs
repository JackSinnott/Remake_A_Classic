using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhoulMovement : MonoBehaviour
{
    float m_moveSpeed = -3f;
    Rigidbody2D m_enemyRGBD;
    // Start is called before the first frame update
    void Awake()
    {
        m_enemyRGBD = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        m_enemyRGBD.velocity = new Vector2(m_moveSpeed, 0.0f);
    }
}
