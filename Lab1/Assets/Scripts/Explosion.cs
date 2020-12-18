using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public GameObject explosion;
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Wall"))
        {
            Instantiate(explosion, transform.position, transform.rotation);
            Destroy(transform.gameObject);
        }
    }
}
