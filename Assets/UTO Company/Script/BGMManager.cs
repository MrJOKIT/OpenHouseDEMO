using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : MonoBehaviour
{
    private AudioSource _audioSource;
    public AudioClip mainBGM, gameOver, ultimate;
    

    private PlayerController _playerController;
    private GameController _gameController;
    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        _gameController = GetComponent<GameController>();
    }

    private void Update()
    {
        ChangBGM();
    }

    private void ChangBGM()
    {
        if (_playerController.onPower )
        {
            _audioSource.clip = ultimate;
            _audioSource.Play();
        }
        else if (_gameController.gameOver)
        {
            _audioSource.clip = gameOver;
            _audioSource.Play();
        }
        else
        {
            _audioSource.clip = mainBGM;
            _audioSource.Play();
        }
    }
}
