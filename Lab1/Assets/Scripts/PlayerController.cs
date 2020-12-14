using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speed = 0f;
    public float maxSpeed = 5f;
    public float acceleration = 1.5f;
    public float deceleration = 0.5f;
    private Vector3 movement;
    private float KeyPressedTime;

    private void Start()
    {
       
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
            speed = Mathf.Min(speed + acceleration * Time.deltaTime, maxSpeed);
            KeyPressedTime = Time.time;
        }
        else
        {
            speed = Mathf.Max(speed - deceleration * Time.deltaTime * 1.5f, 0);
        }

        if (speed == maxSpeed)
        {
            Debug.Log(KeyPressedTime);
        }
        // Normalise the movement vector and make it proportional to the speed per second.
        movement = movement.normalized * speed * Time.deltaTime;
        // Move the player to it's current position plus the movement.
        GetComponent<Rigidbody2D>().MovePosition(transform.position + movement);
    }

}