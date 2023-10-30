using System;
using System.Collections;
using System.Collections.Generic;
using Febucci.UI;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;

public class ScoreResult : MonoBehaviour
{
    private MenuManager _manager;
    private GameController _gameController;
    public TextMeshProUGUI scoreResult;
    private float resultScore;
    public List<GameObject> iconResult;
    private void OnEnable()
    {
        _gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        _manager= GameObject.FindGameObjectWithTag("GameController").GetComponent<MenuManager>();
        
    }

    private void Update()
    {
        CalculateResult();
        switch (resultScore)
        {
            
            case > 750000:
                iconResult[0].SetActive(false);
                iconResult[1].SetActive(false);
                iconResult[2].SetActive(false);
                iconResult[3].SetActive(true);
                break;
            case > 350000:
                iconResult[0].SetActive(false);
                iconResult[1].SetActive(false);
                iconResult[2].SetActive(true);
                iconResult[3].SetActive(false);
                break;
            case > 150000:
                iconResult[0].SetActive(false);
                iconResult[1].SetActive(true);
                iconResult[2].SetActive(false);
                iconResult[3].SetActive(false);
                break;
            default:
                iconResult[0].SetActive(true);
                iconResult[1].SetActive(false);
                iconResult[2].SetActive(false);
                iconResult[3].SetActive(false);
                break;
        }
    }

    private void CalculateResult()
    {
        if (resultScore <= _gameController.playerScore && !_manager.menuActive)
        {
            resultScore += Time.deltaTime * _gameController.playerScore / 1.15f;
        }

        int finalScore = Convert.ToInt32(resultScore);
        scoreResult.text = finalScore.ToString();
    }
    
}
