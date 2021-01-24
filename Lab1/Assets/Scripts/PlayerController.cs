using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float m_moveSpeed;
    private Rigidbody2D m_playerRGBD;
    public float m_movement;
    private float moveAmountHorizontal;
    private float moveAmountVertical;
    private float jumpPower;


    void Awake()
    {
        m_playerRGBD = this.GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        jumpPower = 8.0f;
    }

    private void Update()
    {
        m_movement = Input.GetAxis("Horizontal");


    }

    private void FixedUpdate()
    {
        moveCharacter(m_movement);
        Jump();

        m_playerRGBD.velocity = new Vector2(moveAmountHorizontal, m_playerRGBD.velocity.y);
    }

    void moveCharacter(float m_dir)
    {
        moveAmountHorizontal = m_dir * m_moveSpeed;
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpPower), ForceMode2D.Impulse);
        }
       
    }
}