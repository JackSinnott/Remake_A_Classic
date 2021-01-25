using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Transform Player;

    public Text scoreText;

    private int score;

    private void Start()
    {
        score = 0;
    }
    private void Update()
    {
       
    }

    public void AddScore(int t_newScore)
    {
        score += t_newScore;
        UpdateScore();
    }

    void UpdateScore()
    {
        scoreText.text = "Score: " + score;
    }
}
