using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class StageSpawner : MonoBehaviour
{
    private StageGenerateManager stageManager;

    private void Awake()
    {
        stageManager = GameObject.FindGameObjectWithTag("StageManager").GetComponent<StageGenerateManager>();
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            Instantiate(stageManager.stagePrefab[Random.Range(0,stageManager.stagePrefab.Count)],transform.position,Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
