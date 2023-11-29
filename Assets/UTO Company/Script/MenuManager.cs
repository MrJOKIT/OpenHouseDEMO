using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MenuManager : MonoBehaviour
{
    private StageSlide _stageSlide;
    private PlayerInput _playerInput;
    private GameController _gameController;
    [Header("Menu")] 
    public GameObject menuCanvas;
    public GameObject menuPanel;
    public GameObject playerCanvas;
    public GameObject capturePanel;
    public GameObject screenShotPanel;
    public bool menuActive;

    private void Awake()
    {
        _playerInput = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInput>();
        _stageSlide = GameObject.FindGameObjectWithTag("StageSlide").GetComponent<StageSlide>();
        _gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    private void Start()
    {
        menuCanvas.SetActive(false);
        menuActive = false;
    }

    private void Update()
    {
        if (_stageSlide.start && !_gameController.gameOver)
        {
            MenuController();
        }
        
    }

    private void MenuController()
    {
        if (_playerInput.actions["Esc"].triggered && menuActive)
        {
            playerCanvas.SetActive(true);
            menuPanel.SetActive(true);
            menuCanvas.SetActive(false);
            capturePanel.SetActive(false);
            screenShotPanel.SetActive(false);
            menuActive = false;
            Time.timeScale = 1f;
            Cursor.visible = false;

        }
        else if (_playerInput.actions["Esc"].triggered && !menuActive)
        {
            playerCanvas.SetActive(false);
            menuCanvas.SetActive(true);
            capturePanel.SetActive(false);
            screenShotPanel.SetActive(false);
            menuActive = true;
            Time.timeScale = 0f;
            Cursor.visible = true;
        }
    }
    
     public void PauseGame()
     {
         if (!_stageSlide.start || _gameController.gameOver) return;
         playerCanvas.SetActive(false);
         menuCanvas.SetActive(true);
         capturePanel.SetActive(false);
         screenShotPanel.SetActive(false);
         menuActive = true;
         Time.timeScale = 0f;
         //Cursor.visible = true;


     }

    public void ResumeGame()
    {
        if (!_stageSlide.start || _gameController.gameOver) return;
        menuCanvas.SetActive(false);
        playerCanvas.SetActive(true);
        capturePanel.SetActive(false);
        screenShotPanel.SetActive(false);
        menuActive = false;
        Time.timeScale = 1f;
        //Cursor.visible = false;
    }
}
