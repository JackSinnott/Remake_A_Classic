using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public float speed = 1.0f;
    private bool isGrounded;
    private float jumpPower;
    private float knockPower;
    private int playerHealth;
    bool damaged;

    void Start()
    {
        damaged = false;
        jumpPower = 4.0f;
        knockPower = 5.0f;
        playerHealth = 3;
    }

    // Update is called once per frame
    void Update()
    {
        if(!damaged)
        {
            if (Input.GetKey("a") || Input.GetKey(KeyCode.LeftArrow))
            {
                transform.Translate(Vector3.left * speed * Time.deltaTime);
            }
            if (Input.GetKey("d") || Input.GetKey(KeyCode.RightArrow))
            {
                transform.Translate(Vector3.right * speed * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.Space) && isGrounded)
            {
                gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpPower), ForceMode2D.Impulse);
                isGrounded = false;
            }
        }

        if(playerHealth < 1)
        {
            Destroy(transform.gameObject);
        }

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
            Debug.Log("You have collided");
            Debug.Log("Health: " + playerHealth);
        }
        else
        {
            damaged = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("FireBall"))
        {
            transform.GetComponent<Rigidbody2D>().AddForce(Vector2.left * knockPower, ForceMode2D.Impulse); // change to whatever the speed is.
            Destroy(collider.gameObject);
            playerHealth--;
            Debug.Log("You have collided with FireBall");
            Debug.Log("Health: " + playerHealth);
        }
    }
}

