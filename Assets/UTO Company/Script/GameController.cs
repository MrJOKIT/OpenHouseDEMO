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
    public AudioClip gameOverClip;
    public bool gameOver = false;

    [Header("Player Score")]
    public List<GameObject> iconScores;
    public TextAnimator_TMP scoreText;
    public TextAnimator_TMP gameOverText;
    public int playerScore = 0;
    public Transform mailInventory;

    private StageSlide _stageSlide;


    private void Start()
    {
        Cursor.visible = false;
        _stageSlide = GameObject.FindGameObjectWithTag("StageSlide").GetComponent<StageSlide>();
        //scoreText.SetText("<bounce a=0.1 f=2 w=0.5>"+ playerScore);
    }

    private void Update()
    { 
        ChangeIcon();
        //float playerScoreCal = 10 * Time.deltaTime;
        if (!gameOver && _stageSlide.start)
        {
            playerScore += 1;
        }
        scoreText.SetText($"<bounce a=0.1 f=2 w=0.5>{playerScore}" );
    }
    
    private void ChangeIcon()
    {
        switch (playerScore)
        {
            
            case > 750000:
                iconScores[0].SetActive(false);
                iconScores[1].SetActive(false);
                iconScores[2].SetActive(false);
                iconScores[3].SetActive(true);
                break;
            case > 350000:
                iconScores[0].SetActive(false);
                iconScores[1].SetActive(false);
                iconScores[2].SetActive(true);
                iconScores[3].SetActive(false);
                break;
            case > 150000:
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
        Cursor.visible = true;
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
