using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumping : MonoBehaviour
{
    public bool isGrounded;
    public float jumpPower;
    public Animator Anim;
    private Rigidbody2D playerRGBD;

    private void Awake()
    {
        playerRGBD = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Jump();
    }

    public void Jump()
    {
        if (Input.GetKeyDown(KeyCode.N) && isGrounded)
        {
            playerRGBD.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpPower), ForceMode2D.Impulse);
        }
    }

    /// <summary>
    /// ** Are we grounded or are we currently jumping **
    /// Removes errors as their is no ambiguity over the state of the jump state.
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            Anim.SetBool("IsJumping", false);
        }
    }

    /// <summary>
    /// ** Have we jumped? **
    /// Changed as through my own state code the jump was very broken sadly
    /// </summary>
    /// <param name="collision"></param>
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
            Anim.SetBool("IsJumping", true);
        }
    }

}
