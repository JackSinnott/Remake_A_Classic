using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Name : MonoBehaviour
{
    public string playerName;
    public string saveName;

    public Text inputText;
    public Text loadedName;

    private void Update()
    {
        playerName = PlayerPrefs.GetString("Name", "none");
        loadedName.text = playerName;
    }

    public void SetName()
    {
        saveName = inputText.text;
        PlayerPrefs.SetString("name", saveName);
    }

    public string GetName()
    {
        return PlayerPrefs.GetString(saveName);
    }
}
