using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject playerCanvas;
    [SerializeField] private GameObject gameOverCanvas;
    
    public void GameOver()
    {
        playerCanvas.SetActive(false);
        gameOverCanvas.SetActive(true);
        Time.timeScale = 0f;
    }
}
