using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scoring : MonoBehaviour
{
    public int scoreValue;
    private Score score;

    private void Start()
    {
        score = GetComponent<Score>();
        scoreValue = 10;
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Enemy")
        {
            score.AddScore(scoreValue);
        }
    }
}
