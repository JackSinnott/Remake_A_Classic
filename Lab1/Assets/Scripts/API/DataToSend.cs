using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataToSend : MonoBehaviour
{
    public static string playerNameStr;

    Health playerHealth;

    int healthCheck;

    GameState Data;

    private void Awake()
    {
        playerHealth = GetComponent<Health>();
    }

    private void Start()
    {
        System.DateTime date_time = System.DateTime.Now;
        string foo = date_time.ToString();

        Debug.Log(Application.unityVersion);

        healthCheck = playerHealth.getHealth();

        Data = new GameState
        {
            version = Application.unityVersion,
            timestamp = (string)System.DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss"),
            user_id = playerNameStr,
            event_name = "Health",
            data = playerHealth.getHealth()
        };
    }

    private void Update()
    {
        // update our health value so we send our current health value to the server
        if (healthCheck != playerHealth.getHealth())
        {
            healthCheck = playerHealth.getHealth();
            Data.data = (int)healthCheck;
        }


        if (Input.GetKeyDown(KeyCode.Z))
        {
            string jsonData = JsonUtility.ToJson(Data);
            StartCoroutine(APIWebCall.PostMethod(jsonData));
            Debug.Log("Json data: " + jsonData);
           
        }
    }
}
