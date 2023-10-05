using System;
using System.Collections;
using System.Collections.Generic;
using Com.LuisPedroFonseca.ProCamera2D;
using UnityEngine;

public class Damage : MonoBehaviour
{
    private PlayerController playerController;
    [SerializeField]private Transform vfx;

    private void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            col.GetComponent<PlayerController>().PlayerHit();
            ProCamera2DShake.Instance.Shake(0);
            Instantiate(vfx, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        else if (col.CompareTag("Boss"))
        {
            ProCamera2DShake.Instance.Shake(1);
            Instantiate(vfx, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
