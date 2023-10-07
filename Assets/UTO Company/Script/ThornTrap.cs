using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThornTrap : MonoBehaviour
{
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private float detectRadius;
    private bool onPlayer;
    private bool trapStart;

    [SerializeField] private float trapStartTime;
    private float trapStartCounter;

    private Animator animator;
    private Damage damage;

    private void Start()
    {
        animator = GetComponent<Animator>();
        damage = GetComponent<Damage>();
        damage.enabled = false;
    }

    private void Update()
    {
        CheckPlayer();
    }

    private void CheckPlayer()
    {
        onPlayer = Physics2D.OverlapCircle(transform.position, detectRadius,playerLayer);
        if (onPlayer)
        {
            trapStart = true;
            animator.SetBool("Ready",true);
        }

        if (trapStart)
        {
            trapStartCounter += Time.deltaTime;
            if (trapStartCounter >= trapStartTime)
            {
                animator.SetBool("Hit",true);
                damage.enabled = true;
                trapStartCounter = 0f;
                trapStart = false;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position,detectRadius);
    }
}
