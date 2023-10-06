using System;
using System.Collections;
using System.Collections.Generic;
using Febucci.UI;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;

public class ScoreResult : MonoBehaviour
{
    private GameController _gameController;
    public TextMeshProUGUI scoreResult;
    private float resultScore;
    public List<GameObject> iconResult;
    private void OnEnable()
    {
        _gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        
    }

    private void Update()
    {
        CalculateResult();
    }

    private void CalculateResult()
    {
        if (resultScore <= _gameController.playerScore)
        {
            resultScore += Time.deltaTime * _gameController.playerScore / 1.15f;
        }
        else
        {
            switch (_gameController.playerScore)
            {
            
                case > 5000:
                    iconResult[0].SetActive(false);
                    iconResult[1].SetActive(false);
                    iconResult[2].SetActive(false);
                    iconResult[3].SetActive(true);
                    break;
                case > 2500:
                    iconResult[0].SetActive(false);
                    iconResult[1].SetActive(false);
                    iconResult[2].SetActive(true);
                    iconResult[3].SetActive(false);
                    break;
                case > 1000:
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

        int finalScore = Convert.ToInt32(resultScore);
        scoreResult.text = finalScore.ToString();

    }
    
}
