using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerScore : MonoBehaviour
{
    [SerializeField] private int scoreNumber;
    [SerializeField] private TextMeshProUGUI noText;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI scoreText;

    private void Start()
    {
        noText.text = Convert.ToString(scoreNumber + 1) + ".";
        nameText.text = HighScore.instance.playerScores[scoreNumber].playerName;
        scoreText.text = HighScore.instance.playerScores[scoreNumber].playerScore.ToString();
    }
}
