using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    public GameObject[] itemList;
    private int itemNum;
    private int randNum;
    private Transform EnemyPosition;


    private void Start()
    {
        EnemyPosition = GetComponent<Transform>();
/*        Debug.Log(itemList);*/
    }

    public void DropItem()
    {
        randNum = Random.Range(0, 101);
        Debug.Log("Random Number is " + randNum);

        if (randNum > 30  && randNum < 60) // potion
        {
            itemNum = 1;
            Instantiate(itemList[itemNum], EnemyPosition.position, Quaternion.identity);
        }
        else if (randNum > 65 && randNum < 95) // Kunai drop
        {
            itemNum = 0;
            Instantiate(itemList[itemNum], EnemyPosition.position, Quaternion.identity);
        }
        else { 
            // do nothing
        }
    }

}
 

