using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour
{
    private float timebtwAttack;
    public float startTimeBtwAttack;
    private int m_playerKill;
    public Transform attackRightPos;
    public Transform attackLeftPos;
    public LayerMask whatIsEnemy;
    public float attackRange;
    public int damage; // Not really necessary for normal enemies but some big enemies or bosses could take multiple hits!

    Collider2D[] enemiesToDamage;
    public Animator Anim;
    Controller direction; // we are only concerned with the player direction part of the controller


    private void Awake()
    {
        m_playerKill = 0;
        direction = GetComponent<Controller>();
    }
    private void Update()
    {
        // If so swing away
        if (timebtwAttack <= 0)
        {
            if(Input.GetKeyDown(KeyCode.J) && direction.getDirection())
            {  
                timebtwAttack = startTimeBtwAttack;
                enemiesToDamage = Physics2D.OverlapCircleAll(attackRightPos.position, attackRange, whatIsEnemy);
                Anim.SetTrigger("Attack");
                for (int i = 0; i < enemiesToDamage.Length; i++)
                {
                    enemiesToDamage[i].GetComponent<EnemyCollision>().TakeDamage(damage);
                    if(enemiesToDamage[i].tag == "Ghost")
                    {
                        m_playerKill++;
/*                        Debug.Log("Its a ghost");*/
                    }
                }
            }
            else if (Input.GetKeyDown(KeyCode.J) && !direction.getDirection())
            {
                timebtwAttack = startTimeBtwAttack;
                enemiesToDamage = Physics2D.OverlapCircleAll(attackLeftPos.position, attackRange, whatIsEnemy);
                Anim.SetTrigger("Attack");
                for (int i = 0; i < enemiesToDamage.Length; i++)
                {
                    enemiesToDamage[i].GetComponent<EnemyCollision>().TakeDamage(damage);
                }
            }
        }
        else
        {
            timebtwAttack -= Time.deltaTime;
        }       

    }


    private void OnDrawGizmosSelected()
    {
        if (attackRightPos == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackRightPos.position, attackRange);

        if (attackLeftPos == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackLeftPos.position, attackRange);
    }

    public int getKills()
    {
        return m_playerKill;
    }

}
