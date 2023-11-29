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
    [SerializeField] private GameObject playAgainCanvas;
    public bool gameOver = false;
    private bool adsActive;
    [SerializeField] private GameObject adsButton;

    [Header("Player Score")]
    public List<GameObject> iconScores;
    public TextAnimator_TMP scoreText;
    public TextAnimator_TMP gameOverText;
    public TextAnimator_TMP saveNotificationText;
    public int playerScore = 0;
    public Transform mailInventory;
    public string playerName;

    private StageSlide _stageSlide;
    private MenuManager _manager;
    private PlayerController _playerController;


    private void Start()
    {
        Cursor.visible = false;
        _stageSlide = GameObject.FindGameObjectWithTag("StageSlide").GetComponent<StageSlide>();
        _playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        _manager = GetComponent<MenuManager>();
        //scoreText.SetText("<bounce a=0.1 f=2 w=0.5>"+ playerScore);
    }

    private void Update()
    { 
        ChangeIcon();
        //float playerScoreCal = 10 * Time.deltaTime;
        if (!gameOver && _stageSlide.start && !_manager.menuActive)
        {
            playerScore += 1;
            scoreText.SetText($"<bounce a=0.1 f=2 w=0.5>{playerScore}" );
        }
        

        if (adsActive)
        {
            adsButton.SetActive(false);
        }
    }

    public void PlayAgain()
    {
        gameOver = false;
        gameOverCanvas.SetActive(false);
        playAgainCanvas.SetActive(false);
        Cursor.visible = false;
        
        playerCanvas.SetActive(true);
        _stageSlide.SlideSpeed = 5f;
    }

    public void AdsToContinue()
    {
        _playerController.ResetPosition();
        _playerController.IncreaseHp(0.5f);
        gameOverCanvas.SetActive(false);
        adsActive = true;
        playAgainCanvas.SetActive(true);
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
    
    public void InputPlayerName(string n)
    {
        playerName = n;
    }

    public void SaveNameHighScore()
    {
        if (playerName != null)
        {
            HighScore.instance.SaveHighScore(playerName,playerScore);
        }
        else
        {
            HighScore.instance.SaveHighScore("Unknow",playerScore);
        }
    }

    public void SaveNotification()
    {
        saveNotificationText.SetText("SAVE!!");
    }

    public void CloseNotification()
    {
        saveNotificationText.SetText("{#fade}");
    }
    
}
