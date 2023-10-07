using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMover : MonoBehaviour
{
    public Transform posA, posB;
    public int speed;
    private Vector2 targetPos;

    private void Start()
    {
        targetPos = posB.position;
    }

    private void Update()
    {
        if (Vector2.Distance(transform.position,posA.position) < 0.1f)
        {
            targetPos = posB.position;
        }

        if (Vector2.Distance(transform.position,posB.position) < 0.1f)
        {
            targetPos = posA.position;
        }

        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
    }

    
}

