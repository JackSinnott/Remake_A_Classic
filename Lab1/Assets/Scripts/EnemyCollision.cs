using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
    int health = 1;
    private ItemDrop getItem;

    public Animator Anim;

    // Start is called before the first frame update
    void Start()
    {
        getItem = GetComponent<ItemDrop>();
    }

    private void Update()
    {
        
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Projectile"))
        {
            if (getItem != null)
            {
                getItem.DropItem();
                Debug.Log("Dropped an Item " + getItem);
            }
        }

    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("Damage Taken");

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Anim.SetBool("IsDead", true);
        Debug.Log("EnemyDied");

        this.enabled = false;
        GetComponent<Collider2D>().enabled = false;

    }
}
