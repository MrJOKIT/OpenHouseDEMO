using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LeaderBoard : MonoBehaviour
{
    public string playerName;

    private GameController _gameController;

    private int a, b,c;
    public List<int> hightScore;
    private void Start()
    {
        _gameController = GetComponent<GameController>();
        
    }

    public void SaveName(TextMeshProUGUI name)
    {
        playerName = name.text;

        ES3.Save($"{playerName}",_gameController.playerScore);
        SortingRank();
        RankResult();
    }

    private void SortingRank()
    {
        for (int i = 2; i > 0; i--)
        {
            a = ES3.Load<int>($"rank{i}");
            b = ES3.Load<int>($"{playerName}");

            if (a < b)
            {
                ES3.Save($"rank{i}",_gameController.playerScore);
                break;
            }
        }
        
    }

    private void RankResult()
    {
        for (int i = 0; i < hightScore.Count; i++)
        {
            hightScore[i] = ES3.Load<int>($"rank{i}");
        }
    }

    
}
