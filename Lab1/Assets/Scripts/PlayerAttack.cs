using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour
{
    private float timebtwAttack;
    public float startTimeBtwAttack;

    public Transform attackRightPos;
    public Transform attackLeftPos;
    public LayerMask whatIsEnemy;
    public float attackRange;
    public int damage; // Not really necessary for normal enemies but some big enemies or bosses could take multiple hits!

    Collider2D[] enemiesToDamage;
    public Animator Anim;


    private void Update()
    {
        // If so swing away
        if (timebtwAttack <= 0)
        {
            if(Input.GetKeyDown(KeyCode.J) && PlayerPrefs.GetInt("faingRight") == 1)
            {  
                timebtwAttack = startTimeBtwAttack;
                enemiesToDamage = Physics2D.OverlapCircleAll(attackRightPos.position, attackRange, whatIsEnemy);
                Anim.SetTrigger("Attack");
                for (int i = 0; i < enemiesToDamage.Length; i++)
                {
                    enemiesToDamage[i].GetComponent<EnemyCollision>().TakeDamage(damage);
                }
            }
            else if (Input.GetKeyDown(KeyCode.J) && PlayerPrefs.GetInt("faingRight") == 0)
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
}
