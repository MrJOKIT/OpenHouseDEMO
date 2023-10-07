using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class MailPoint : MonoBehaviour
{
    public List<AudioClip> audioClip;
    public MailLevel mailLevel;
    private int mailScore;
    public bool onTake;
    public enum MailLevel
    {
        Bronze,
        Silver,
        Gold,
        Platinum,
    }

    private PlayerController _playerController;
    private GameController _gameController;
    
    private void Start()
    {
        _playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        _gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        

    }

    private void Update()
    {
        MailScore();
        if (onTake)
        {
            transform.position = Vector3.Lerp(transform.position,_gameController.mailInventory.position, 10 * Time.deltaTime);
        }
    }
    

    private void MailScore()
    {
        switch (mailLevel)
        {
            case MailLevel.Bronze :
                mailScore = 100;
                break;
            case MailLevel.Silver :
                mailScore = 500;
                break;
            case MailLevel.Gold:
                mailScore = 1500;
                break;
            case MailLevel.Platinum:
                mailScore = 3000;
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("MailInventory"))
        {
            _gameController.playerScore += mailScore;
            SoundEffectPlayer.instance.PlayerAudio(audioClip[Random.Range(0,audioClip.Count)]);
            //_gameController.SetTextAnimator();
            Destroy(gameObject);
        }
    }
}
