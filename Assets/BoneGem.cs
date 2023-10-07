using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneGem : MonoBehaviour
{
    private PlayerController _playerController;
    private void Start()
    {
        _playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            _playerController.GetUltimatePoint();
            Destroy(gameObject);
        }
    }
}
