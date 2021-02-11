using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataToSend : MonoBehaviour
{
    public static string playerNameStr;

    Health playerHealth;
    PlayerAttack m_playerKills;
    PlayerJumping m_playerData;


    int healthCheck;
    int m_killCheck;
    int m_jumpCheck;
    bool m_once;

    GameState healthData;

    private void Awake()
    {
        playerHealth = GetComponent<Health>();
        m_playerKills = GetComponent<PlayerAttack>();
        m_playerData = GetComponent<PlayerJumping>();
        m_once = false;
    }

    private void Start()
    {
        System.DateTime date_time = System.DateTime.Now;
        string foo = date_time.ToString();

        Debug.Log(Application.unityVersion);

        healthCheck = playerHealth.getHealth();
        m_killCheck = m_playerKills.getKills();
        m_jumpCheck = m_playerData.getJumpData();
        /*m_timeCheck = m_elpaseTime.getTime();*/

        healthData = new GameState
        {
            version = Application.unityVersion,
            timestamp = (string)System.DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss"),
            user_id = playerNameStr,
            event_name = "Health",
            data = playerHealth.getHealth(),
            killData = m_playerKills.getKills(),
            jumpData = m_playerData.getJumpData()
            /*timePlayedData = m_timeCheck*/
        };
    }

    private void Update()
    {
        // update our health value so we send our current health value to the server
        if (healthCheck != playerHealth.getHealth())
        {
            healthCheck = playerHealth.getHealth();
            healthData.data = (int)healthCheck;
            m_killCheck = m_playerKills.getKills();
            healthData.killData = (int)m_killCheck;
            m_jumpCheck = m_playerData.getJumpData();
            healthData.jumpData = (int)m_jumpCheck;
            /*m_timeCheck = m_elpaseTime.getTime();
            healthData.timePlayedData = (int)m_timeCheck;*/
        }

        if (playerHealth.isDead() || Input.GetKeyDown(KeyCode.Z))
        {
            sentData();
        }
    }

    void sentData()
    {
        if (!m_once)
        {
            string jsonData;
            jsonData = JsonUtility.ToJson(healthData);
            StartCoroutine(APIWebCall.PostMethod(jsonData));
            m_once = true;
        }

    }
}
