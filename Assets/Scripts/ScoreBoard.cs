using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour
{
    [SerializeField] int scoreHit = 150;
    [SerializeField] int timeScore = 1;

    int score;
    int totalScore;
    Text scoreText;

    PlayerController player;

    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        scoreText = GetComponent<Text>();
        scoreText.text = score.ToString();
    }

    void Update()
    {
        if (player.isAlive)
        {
            timeScore = timeScore + 1;
            totalScore = timeScore + score;
            scoreText.text = totalScore.ToString();
        }
        
    }

    public void ScoreHit(int scorePerHit)
    {
        score = score + scoreHit + scorePerHit;
    }
}
