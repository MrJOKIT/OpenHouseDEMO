using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class LifeGem : MonoBehaviour
{
    public List<AudioClip> audioClip;
    [SerializeField] private float hpIncreasePoint;
    private PlayerController _playerController;
    public bool onTake;
    private void Start()
    {
        _playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    private void Update()
    {
        if (onTake)
        {
            transform.position = Vector3.Lerp(transform.position,_playerController.transform.position, 100 * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            SoundEffectPlayer.instance.PlayerAudio(audioClip[Random.Range(0,audioClip.Count)]);
            _playerController.IncreaseHp(hpIncreasePoint);
            Destroy(gameObject);
        }
    }
}
