using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HighScore : MonoBehaviour
{
    public static HighScore instance;
    private int currentHighScore;
    public List<Score> playerScores;
    private bool canInsert = true;


    private void OnEnable()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        playerScores = ES3.Load("LeaderBoardArray", playerScores);
    }

    public void SaveHighScore(string playerName,int playerScore)
    {
        
        for (int i = 0; i < playerScores.Count - 1; i++)
        {
            if (playerScores[i].playerName == playerName && playerScores[i].playerScore <= playerScore)
            {
                playerScores[i].playerScore = playerScore;
                canInsert = false;
                break;
            }
            else
            {
                canInsert = true;
            }
        }

        if (canInsert)
        {
            var newScore = new Score
            {
                playerName = playerName,
                playerScore = playerScore
            };
            playerScores.Insert(0,newScore);
            canInsert = true;
        }
        
        
        ReloadLeaderBoard();
        ES3.Save("LeaderBoardArray",playerScores);
    }
    
    private void ReloadLeaderBoard()
    {
        for (int i = 0; i < playerScores.Count - 1; i++)
        {
            for (int j = i + 1; j < playerScores.Count; j++)
            {
                if (playerScores[i].playerScore < playerScores[j].playerScore)
                {
                    var temp = playerScores[i].playerScore;
                    var tempName = playerScores[i].playerName;
                    playerScores[i].playerScore = playerScores[j].playerScore;
                    playerScores[i].playerName = playerScores[j].playerName;
                    playerScores[j].playerScore = temp;
                    playerScores[j].playerName = tempName;
                }
            }
            
        }

        if (playerScores.Count > 4)
        {
            playerScores.RemoveAt(5);
        }
    }
    
    
    
}

[Serializable]
public class Score
{
    public string playerName;
    public int playerScore;
}
