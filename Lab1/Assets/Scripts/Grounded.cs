using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grounded : MonoBehaviour
{
    GameObject m_player;
    // Start is called before the first frame update
    void Start()
    {
        m_player = gameObject.transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Background")
        {
            m_player.GetComponent<PlayerController>().isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Background")
        {
            m_player.GetComponent<PlayerController>().isGrounded = false ;
        }
    }
}
