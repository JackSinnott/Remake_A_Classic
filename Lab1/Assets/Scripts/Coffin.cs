using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coffin : MonoBehaviour
{
    private PlayerController player;
    private Health playerHealh;

    private void Start()
    {
        player = GetComponent<PlayerController>();
        playerHealh = GetComponent<Health>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            player.save();
            playerHealh.heal(8);
            playerHealh.restoreMana(8);
        }

    }
}
