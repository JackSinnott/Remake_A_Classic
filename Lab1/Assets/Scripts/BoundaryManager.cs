using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundaryManager : MonoBehaviour
{
    private BoxCollider2D managerBox;
    private Transform player;
    public GameObject boundaryObject;
    public GameObject enemyManager;


    // Start is called before the first frame update
    void Start()
    {
        managerBox = GetComponent<BoxCollider2D>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        ManageBoundary();
    }

    void ManageBoundary()
    {
        if (managerBox.bounds.min.x < player.position.x && player.position.x < managerBox.bounds.max.x &&
            managerBox.bounds.min.y < player.position.y && player.position.y < managerBox.bounds.max.y)
        {
            boundaryObject.SetActive(true);
            enemyManager.SetActive(true);
        }
        else
        {
            boundaryObject.SetActive(false);
            enemyManager.SetActive(false);
        }
    }

}
