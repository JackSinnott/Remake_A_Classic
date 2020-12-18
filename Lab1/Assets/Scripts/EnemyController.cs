using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    Transform player;
    [SerializeField]
    float agroRange;
    [SerializeField]
    float movespeed;

    SpriteRenderer rend;
    Rigidbody2D rb2D;
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        rend = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        float disToPlayer = Vector2.Distance(transform.position, player.position);
        //print("Dis To Player : " + disToPlayer);

        if (disToPlayer < agroRange)
        {
            ChasePlayer();
        }
        else
        {
            StopChasingPlayer();
        }
    }

    private void ChasePlayer()
    {
        if (transform.position.x < player.position.x)
        {
            rb2D.velocity = new Vector2(movespeed, 0f);
            transform.localScale = new Vector2(.2f, 2f);
        }
        else if (transform.position.x > player.position.x)
        {
            rb2D.velocity = new Vector2(-movespeed, 0f);
            transform.localScale = new Vector2(-.2f, 2f);
        }
    }

    void StopChasingPlayer()
    {
        rb2D.velocity = new Vector2(0f, 0f);
    }

}
