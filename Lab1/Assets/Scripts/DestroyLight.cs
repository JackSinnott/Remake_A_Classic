using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyLight : MonoBehaviour
{
    public GameObject[] dropItems;

    public ParticleSystem smokeEffect;

    public void OnInteract()
    {
        //choose a random item to drop...
        var item = Random.Range(0, dropItems.Length);

        bool checkInstantiate = true;

        if (dropItems[item] == null)
            checkInstantiate = false;
        else if (dropItems[item].name.Equals("Heart"))
            checkInstantiate = false;

        if (checkInstantiate)
            Instantiate(dropItems[item], transform.position, Quaternion.identity);

        var death = Instantiate(smokeEffect, transform.position, Quaternion.identity);
        death.Play();
        Destroy(gameObject);
    }
}
