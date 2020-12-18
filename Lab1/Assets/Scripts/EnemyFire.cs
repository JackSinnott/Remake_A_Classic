using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFire : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject shot; // the neon bullet
    GameObject fireball; // the neon bullet
    public Transform shotSpawn; // where the bullet fires
    private float fireRate;
    private float speed; // speed of the bullet
    private bool readyToFire;
    private float nextFire; // when ready to shoot again

    void Start()
    {
        fireRate = 3.0f;
        speed = 5.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!readyToFire && Time.time > nextFire) // checks
        {
            readyToFire = true;
            fireball = Instantiate(shot, shotSpawn.position, shotSpawn.rotation); // create
            FindObjectOfType<AudioManager>().play("EnemyShooting");
            fire();
        }
    }
    void fire()
    {
        nextFire = Time.time + fireRate;
        fireball.GetComponent<Rigidbody2D>().velocity = speed * Vector2.left; // bullet is fired
        readyToFire = false;
    }

}
