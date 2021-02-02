using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player_Name : MonoBehaviour
{
    public InputField playerName;

    public void Playgame()
    {
        Debug.Log("Player name is: " + playerName.text);

        DataToSend.playerNameStr = playerName.text;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }


}
