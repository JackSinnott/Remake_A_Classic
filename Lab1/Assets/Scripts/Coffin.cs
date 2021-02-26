using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coffin : MonoBehaviour
{
    private PlayerController player;

    private void Start()
    {
        player = GetComponent<PlayerController>();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            player.GetComponent<PlayerController>().SavePlayer();
            
    }
}