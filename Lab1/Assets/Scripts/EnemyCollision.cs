﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
    int health = 1;
    private ItemDrop getItem;

    public Animator Anim;
    private float deathTimer;

    // Start is called before the first frame update
    void Start()
    {
        getItem = GetComponent<ItemDrop>();
        deathTimer = .75f;

    }

    private void Update()
    {
        if (health <= 0)
        {
            Die();
        }
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
/*        Debug.Log("Damage Taken");*/
    }

    void Die()
    {
        Anim.SetBool("IsDead", true);
/*        Debug.Log("EnemyDied");*/
        GetComponent<Collider2D>().enabled = false;
        deathTimer -= Time.deltaTime;

        if (deathTimer <= 0f)
        {
            Destroy(this.gameObject);
/*            Debug.Log(deathTimer);*/
            deathTimer = 1f;
        }
    }

    
}
