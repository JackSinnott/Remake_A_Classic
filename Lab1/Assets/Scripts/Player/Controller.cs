using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Controller : MonoBehaviour
{
    PlayerJumping playerJump;
    Health playerHealth;
    Player_Name playerName;
   
    //private float knockPower;

    //bool damaged;
    public GameObject shot; 
    GameObject fireball;
    public Transform shotSpawn; // where the bullet fires
    private float fireRate;
    private float speedFire; // speed of the bullet
    private bool readyToFire;
    private float nextFire; // when ready to shoot again

    private Animator Anim;
    float timer = 2f;

    GameState Data;

    public void Awake()
    {
        Anim = this.GetComponent<Animator>();
        playerHealth = GetComponent<Health>();
        playerJump = GetComponent<PlayerJumping>();

        System.DateTime temp = System.DateTime.Now;
        string foo = temp.ToString();
        Data = new GameState { version = "Castlevania_Developer_Build", timestamp = foo, user_id = "" };
    }

    void Start()
    {
        //damaged = false;
        //knockPower = 5.0f;
        speedFire = 8.0f;
        fireRate = 2.0f;
    }

    // Update is called once per frame
    void Update()
    {
       
        if (!readyToFire && Time.time > nextFire) // checks
        {
            readyToFire = true;
        }

        if (Input.GetKey("k") && readyToFire) // left click
        {
            Fire(); // calls
        }

        checkStatus();
        
        if(Input.GetKeyDown(KeyCode.Z))
        {
            string jsonData = JsonUtility.ToJson(Data);
            StartCoroutine(APIWebCall.PostMethod(jsonData));
            Debug.Log("Json data: " + jsonData);
        }
        
        
    }

    private void FixedUpdate()
    {
       
        playerJump.Jump();
       
        
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
                SceneManager.LoadScene("Game");
                Destroy(this.gameObject);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            playerHealth.takeDamage(1);
            FindObjectOfType<AudioManager>().play("PlayerHit_Enemy");
            Debug.Log("You have collided");

        }


        if (collision.gameObject.CompareTag("Potion"))
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
            //damaged = true;
            Destroy(collider.gameObject);
            playerHealth.takeDamage(1);
            FindObjectOfType<AudioManager>().play("PlayerHit_Bullet");
            Debug.Log("You have collided with :" + collider.gameObject.tag);

        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("You have exited collision with type: " + collision.gameObject.tag);

        }


        if (collision.gameObject.CompareTag("Potion"))
        {
            Debug.Log("You have exited collision with type: " + collision.gameObject.tag);

        }


        if (collision.gameObject.CompareTag("Item"))
        {
            Debug.Log("You have exited collison with type" + collision.gameObject.tag);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("FireBall"))
        {
            //damaged = false;
            Debug.Log("You have exited collision with type :" + collision.gameObject.tag);
        }
    }

}

