using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIGhost : MonoBehaviour
{
    private float m_targetDistance;
    public float m_range;
    public Transform m_target;
    private AIBehavior m_AIBehavior;

    // Start is called before the first frame update
    private void Start()
    {
        m_AIBehavior = GetComponent<AIBehavior>();
    }

    // Update is called once per frame
    void Update()
    {

        m_targetDistance = Vector2.Distance(transform.position, m_target.position);

        if(m_targetDistance <= m_range)
        {
            if(m_target.position.x > transform.position.x && transform.localScale.x > 0 
                || m_target.position.x < transform.position.x && transform.localScale.x < 0)
            {
                m_AIBehavior.changeDirection();
            }
        }
        else
        {
            m_AIBehavior.setStatus(true);
        }
    }
}


