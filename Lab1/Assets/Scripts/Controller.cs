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

    // Start of movement Code
    public float m_moveSpeed;
    private float moveAmountHorizontal;
    private float jumpPower;
    public float m_movement;
    bool m_facingRight = true;

    // End of movement code


    private bool isGrounded;
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

    private Animator Anim;
    private SpriteRenderer spriteRend;
    private Rigidbody2D playerRGBD;

    public void Awake()
    {
        spriteRend = this.GetComponent<SpriteRenderer>();
        playerRGBD = this.GetComponent<Rigidbody2D>();
        Anim = this.GetComponent<Animator>();
    }

    void Start()
    {
        damaged = false;
        jumpPower = 8.0f;
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
            m_movement = Input.GetAxis("Horizontal");            
        }
        Anim.SetFloat("Speed", Mathf.Abs(m_movement));

        if(m_movement < 0f)
        {
            m_facingRight = false;
        }
        else 
        {
            m_facingRight = true;
        }

        if (m_facingRight)
        {
            spriteRend.flipX = false;
        }
        else
        {
            spriteRend.flipX = true;
        }

        if (!readyToFire && Time.time > nextFire) // checks
        {
            readyToFire = true;
        }

        if (Input.GetKey("k") && readyToFire) // left click
        {
            Fire(); // calls
        }
    }

    private void FixedUpdate()
    {
        moveCharacter(m_movement);
        Jump();

        playerRGBD.velocity = new Vector2(moveAmountHorizontal, playerRGBD.velocity.y);
    }

    void moveCharacter(float m_dir)
    {
        moveAmountHorizontal = m_dir * m_moveSpeed;
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Anim.SetBool("IsJumping", true);
            isGrounded = false;
            float verticalCheck = this.transform.position.y; // Used to check if our y value is growing or shrinking
            float yVal = verticalCheck;

            playerRGBD.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpPower), ForceMode2D.Impulse);                    
        }    
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            Anim.SetBool("IsJumping", false);
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

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(attackPos.position, attackRange);
    }

}

