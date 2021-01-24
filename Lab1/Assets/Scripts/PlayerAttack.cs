using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private float timebtwAttack;
    public float startTimeBtwAttack;

    public Transform attackPos;
    public LayerMask whatIsEnemy;
    public Vector2 attackRange;
    public int damage; // Not really necessary for normal enemies but some big enemies or bosses could take multiple hits!

    public Animator Anim;

    private void Update()
    {
        // If so swing away
        if(timebtwAttack <= 0)
        {
            if(Input.GetKeyDown(KeyCode.J))
            {
                timebtwAttack = startTimeBtwAttack;
                Collider2D[] enemiesToDamage = Physics2D.OverlapBoxAll(attackPos.position, attackRange, whatIsEnemy);
                Anim.SetBool("IsAttacking", true);
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
        Gizmos.color = Color.red;
        Gizmos.DrawCube(attackPos.position, attackRange);
    }
}
