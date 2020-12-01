using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speed = 0f;
    public float maxSpeed = 10f;
    public float forceMagnitude = 2f;
    private Vector3 movement;
    //Rigidbody2D playerRigidbody;

    private void Start()
    {
        //playerRigidbody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        // Store the input axes.
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        // Move the player around the scene.
        Move(h, v);
       
    }

    void Move(float h, float v)
    {
        if (v != 0 || h != 0)
        {
            // Set the movement vector based on the axis input.
            movement.Set(h, 0f, v);
            speed = Mathf.Min(speed + forceMagnitude * Time.deltaTime, maxSpeed);
        }
        else
        {
            speed = Mathf.Max(speed - forceMagnitude * Time.deltaTime * 1.5f, 0);
        }

        // Normalise the movement vector and make it proportional to the speed per second.
        movement = movement.normalized * speed * Time.deltaTime;
        // Move the player to it's current position plus the movement.
        GetComponent<Rigidbody2D>().MovePosition(transform.position + movement);
    }

}