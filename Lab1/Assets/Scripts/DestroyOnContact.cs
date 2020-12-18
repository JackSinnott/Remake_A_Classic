using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnContact : MonoBehaviour
{

    public GameObject bloodExplosion;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
            Destroy(transform.gameObject);
            Instantiate(bloodExplosion, transform.position, transform.rotation);
        }

        if(other.CompareTag("Wall"))
        {
            Destroy(transform.gameObject);
        }
        
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
            Destroy(other.gameObject);
    }

}
