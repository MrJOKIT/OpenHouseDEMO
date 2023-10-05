using System;
using System.Collections;
using System.Collections.Generic;
using Com.LuisPedroFonseca.ProCamera2D;
using UnityEngine;

public class ObstacleObject : MonoBehaviour
{
    [SerializeField] private Transform vfx;

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.CompareTag("Boss"))
        {
            ProCamera2DShake.Instance.Shake(1);
            Instantiate(vfx, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
