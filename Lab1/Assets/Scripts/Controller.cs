using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Controller : MonoBehaviour
{
    // Start of movement Code
    public float m_moveSpeed;
    private float moveAmountHorizontal;
    private float jumpPower;
    public float m_movement;
    bool m_facingRight = true;

    // End of movement code
    Health playerHealth;
    private bool isGrounded;
    private float knockPower;
    bool damaged;
    public GameObject shot; 
    GameObject fireball;
    public Transform shotSpawn; // where the bullet fires
    private float fireRate;
    private float speedFire; // speed of the bullet
    private bool readyToFire;
    private float nextFire; // when ready to shoot again
    bool hit = false;
    private Animator Anim;
    private SpriteRenderer spriteRend;
    private Rigidbody2D playerRGBD;
    float timer = 2f;

    public void Awake()
    {
        spriteRend = this.GetComponent<SpriteRenderer>();
        playerRGBD = this.GetComponent<Rigidbody2D>();
        Anim = this.GetComponent<Animator>();
        playerHealth = GetComponent<Health>();
    }

    void Start()
    {
        damaged = false;
        jumpPower = 5.0f;
        knockPower = 5.0f;
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

        checkStatus();
    }

    private void FixedUpdate()
    {
        moveCharacter(m_movement);
        Jump();

        playerRGBD.velocity = new Vector2(moveAmountHorizontal, playerRGBD.velocity.y);
        if(hit)
        {
            playerRGBD.AddForce(Vector2.left * knockPower, ForceMode2D.Impulse); // change to whatever the speed is.
            hit = false;
        }
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
            hit = true;
            damaged = true;
            playerHealth.takeDamage(1);
            FindObjectOfType<AudioManager>().play("PlayerHit_Enemy");
            Debug.Log("You have collided");
            
        }
        else
        {
            damaged = false;
        }

        if(collision.gameObject.CompareTag("Potion"))
        {
            playerHealth.heal(2);
            Destroy(collision.gameObject);
            
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
            
            Destroy(collider.gameObject);
            playerHealth.takeDamage(1);
            FindObjectOfType<AudioManager>().play("PlayerHit_Bullet");
            Debug.Log("You have collided with FireBall");
           
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

    // see if we are dead
    private void checkStatus()
    {
        if (playerHealth.getHealth() == 0)
        {
            Anim.SetTrigger("IsDead");
         
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                //SceneManager.LoadScene("Game");
                Destroy(this.gameObject);
            }
        }
    }

}

