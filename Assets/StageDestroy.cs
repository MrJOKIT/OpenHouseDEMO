using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageDestroy : MonoBehaviour
{
    [SerializeField] private GameObject destroyObject;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            Destroy(destroyObject);
        }
    }
}
