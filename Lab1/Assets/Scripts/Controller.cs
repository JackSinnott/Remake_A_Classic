using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    //This is the playerAttack code
    private float timebtwAttack;
    public float startTimeBtwAttack;

    public Transform attackPos;
    public LayerMask whatIsEnemy;
    public Vector2 attackRange;
    public int damage; // Not really necessary for normal enemies but some big enemies or bosses could take multiple hits!

    //End of playerAttack Code



    public float speed = 1.0f;
    private bool isGrounded;
    private float jumpPower;
    private float knockPower;
    private int playerHealth;
    bool damaged;
    public GameObject shot; 
    GameObject fireball;
    public Transform shotSpawn; // where the bullet fires
    private float fireRate;
    private float speedFire; // speed of the bullet
    private bool readyToFire;
    private float nextFire; // when ready to shoot again

    public Animator Anim;
    private SpriteRenderer spriteRend;
    private Rigidbody2D playerRGBD;

    public void Awake()
    {
        spriteRend = GetComponent<SpriteRenderer>();
        playerRGBD = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        damaged = false;
        jumpPower = 3.0f;
        knockPower = 5.0f;
        playerHealth = 3;
        speedFire = 8.0f;
        fireRate = 2.0f;
        
    }

    // Update is called once per frame
    void Update()
    {

        if (!damaged)
        {
            if (Input.GetKey("a") || Input.GetKey(KeyCode.LeftArrow))
            {
                transform.Translate(Vector3.left * speed * Time.deltaTime);
                // if the variable isn't empty (we have a reference to our SpriteRenderer
                if (spriteRend != null)
                {
                    // flip the sprite
                    spriteRend.flipX = true;

                }
                Anim.SetBool("IsWalking", true);
            }



            else if (Input.GetKey("d") || Input.GetKey(KeyCode.RightArrow))
            {
                transform.Translate(Vector3.right * speed * Time.deltaTime);
                // if the variable isn't empty (we have a reference to our SpriteRenderer
                if (spriteRend != null)
                {
                    // flip the sprite
                    spriteRend.flipX = false;

                }
                Anim.SetBool("IsWalking", true);
            }
            else
            {
                Anim.SetBool("IsWalking", false);
            }

            if (Input.GetKey(KeyCode.Space) && isGrounded)
            {
                gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpPower), ForceMode2D.Impulse);
                Anim.SetFloat("JumpingVert", playerRGBD.velocity.magnitude);
                isGrounded = false;
            }
            else
            {
                Anim.SetFloat("JumpingVert", 0);
            }
        }

        if (playerHealth < 1)
        {
            Destroy(transform.gameObject);
        }


        if (!readyToFire && Time.time > nextFire) // checks
        {
            readyToFire = true;
        }

        if (Input.GetKey("k") && readyToFire) // left click
        {
            Fire(); // calls
        }

        Debug.Log(playerRGBD.velocity.magnitude);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            transform.GetComponent<Rigidbody2D>().AddForce(Vector2.left * knockPower, ForceMode2D.Impulse); // change to whatever the speed is.
            damaged = true;
            playerHealth--;
            FindObjectOfType<AudioManager>().play("EnemyHitPlayer");
            Debug.Log("You have collided");
            Debug.Log("Health: " + playerHealth);
        }
        else
        {
            damaged = false;
        }

        if(collision.gameObject.CompareTag("Potion"))
        {
            playerHealth += 2;
            Destroy(collision.gameObject);
            Debug.Log("Health: " + playerHealth);
        } 


        if (collision.gameObject.CompareTag("Item"))
        {
            shot.gameObject.GetComponent<SpriteRenderer>().sprite = collision.gameObject.GetComponent<SpriteRenderer>().sprite;
            Destroy(collision.gameObject);
        }


    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("FireBall"))
        {
            transform.GetComponent<Rigidbody2D>().AddForce(Vector2.left * knockPower, ForceMode2D.Impulse); // change to whatever the speed is.
            Destroy(collider.gameObject);
            playerHealth--;
            FindObjectOfType<AudioManager>().play("BulletHitPlayer");
            Debug.Log("You have collided with FireBall");
            Debug.Log("Health: " + playerHealth);
        }
    }

    void Fire()
    {
        fireball = Instantiate(shot, shotSpawn.position, shotSpawn.rotation); // create
        nextFire = Time.time + fireRate;

        if(Input.GetKey("a"))
        {
            fireball.GetComponent<Rigidbody2D>().velocity = speedFire * Vector2.left; // bullet is fired
        }
        else
        {
            fireball.GetComponent<Rigidbody2D>().velocity = speedFire * Vector2.right; // bullet is fired
        }
        readyToFire = false;
    }

    private void Attack()
    {
        // If so swing away
        if (timebtwAttack <= 0)
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                timebtwAttack = startTimeBtwAttack;
                Collider2D[] enemiesToDamage = Physics2D.OverlapBoxAll(attackPos.position, attackRange, whatIsEnemy);
                //Anim.SetBool("IsAttacking", true);
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

