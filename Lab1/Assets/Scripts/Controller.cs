using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Controller : MonoBehaviour
{
    // Start of movement Code
    public float m_moveSpeed;
    private float moveAmountHorizontal;
    public float m_movement;

    // End of movement code

    // Start of jump cpde
    PlayerJumping playerJump;
    Health playerHealth;

   
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
        playerJump = GetComponent<PlayerJumping>();
    }

    void Start()
    {
        PlayerPrefs.SetInt("facingRight", 1);
        damaged = false;
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
        Debug.Log("Damaged state: " + damaged);
        if(m_movement < 0f)
        {
            PlayerPrefs.SetInt("facingRight", 0); 

        }
        else 
        {
            PlayerPrefs.SetInt("facingRight", 1);
        }

        if (PlayerPrefs.GetInt("facingRight") == 1)
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
        //Jump();
        playerJump.Jump();
       

        if(hit)
        {
            playerRGBD.AddForce(Vector2.left * knockPower, ForceMode2D.Impulse); // change to whatever the speed is.
            hit = false;
        }
        playerRGBD.velocity = new Vector2(moveAmountHorizontal, playerRGBD.velocity.y);
    }

    void moveCharacter(float m_dir)
    {
        moveAmountHorizontal = m_dir * m_moveSpeed;
    }

    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            hit = true;
            playerHealth.takeDamage(1);
            FindObjectOfType<AudioManager>().play("PlayerHit_Enemy");
            Debug.Log("You have collided");
            
        }
        else
        {
            hit = false;
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
            hit = true;
            damaged = true;
            Destroy(collider.gameObject);
            playerHealth.takeDamage(1);
            FindObjectOfType<AudioManager>().play("PlayerHit_Bullet");
            Debug.Log("You have collided with FireBall");
           
        }
     
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("FireBall"))
        {
            damaged = false;
            hit = false;
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

