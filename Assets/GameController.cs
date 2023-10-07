using System;
using System.Collections;
using System.Collections.Generic;
using Febucci.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject playerCanvas;
    [SerializeField] private GameObject gameOverCanvas;
    private bool gameOver = false;

    [Header("Player Score")]
    public List<GameObject> iconScores;
    public TextAnimator_TMP scoreText;
    public TextAnimator_TMP gameOverText;
    public int playerScore;
    public Transform mailInventory;

    private StageSlide _stageSlide;


    private void Start()
    {
        _stageSlide = GameObject.FindGameObjectWithTag("StageSlide").GetComponent<StageSlide>();
        scoreText.SetText("<bounce a=0.1 f=2 w=0.5>"+playerScore);
    }

    private void Update()
    { 
        ChangeIcon();
    }

    public void SetTextAnimator()
    {
        scoreText.SetText("<bounce a=0.1 f=2 w=0.5>"+playerScore);
    }

    private void ChangeIcon()
    {
        switch (playerScore)
        {
            
            case > 5000:
                iconScores[0].SetActive(false);
                iconScores[1].SetActive(false);
                iconScores[2].SetActive(false);
                iconScores[3].SetActive(true);
                break;
            case > 2500:
                iconScores[0].SetActive(false);
                iconScores[1].SetActive(false);
                iconScores[2].SetActive(true);
                iconScores[3].SetActive(false);
                break;
            case > 1000:
                iconScores[0].SetActive(false);
                iconScores[1].SetActive(true);
                iconScores[2].SetActive(false);
                iconScores[3].SetActive(false);
                break;
            default:
                iconScores[0].SetActive(true);
                iconScores[1].SetActive(false);
                iconScores[2].SetActive(false);
                iconScores[3].SetActive(false);
                break;
        }
    }

    public void GameOver()
    {
        playerCanvas.SetActive(false);
        gameOverCanvas.SetActive(true);
        _stageSlide.SlideSpeed = 0f;
        if (!gameOver)
        {
            gameOverText.SetText("<shake a=0.5>GAME OVER");
            gameOver = true;
        }
        
        //Time.timeScale = 0f;
    }
}
