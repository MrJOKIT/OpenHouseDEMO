using System;
using System.Collections;
using System.Collections.Generic;
using Com.LuisPedroFonseca.ProCamera2D;
using UnityEngine;

public class BossHunter : MonoBehaviour
{
    [SerializeField] private Transform player;
    
    [SerializeField] private float changeDelay;
    private float changeDelayCounter;
    
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private float frontCheckDistance;
    public LayerMask groundLayer;
    public Transform checkTop;
    public Transform checkUnder;
    private bool onGround;
    private bool onWall;
    private bool readyToChange;

    private Rigidbody2D rb;
    private Rigidbody2D playerRb;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerRb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        //AllCheck();
        ChargePlayer();

        if (readyToChange)
        {
            changeDelayCounter += Time.deltaTime;
            if (changeDelayCounter >= changeDelay)
            {
                ChangeGravity();
                changeDelayCounter = 0f;
            }
        }
    }

    private void ChangeGravity()
    {
        
        rb.gravityScale *= -1;

        if (rb.gravityScale < 0)
        {
            spriteRenderer.flipY = true;
            
        }
        else if (rb.gravityScale > 0)
        {
            spriteRenderer.flipY = false;
        }
        
        
    }

    private void AllCheck()
    {
        onGround = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance,groundLayer)
            || Physics2D.Raycast(transform.position, Vector2.up, groundCheckDistance,groundLayer);
        onWall = Physics2D.Raycast(checkTop.position, Vector2.right, frontCheckDistance,groundLayer)
                 || Physics2D.Raycast(checkUnder.position, Vector2.right, frontCheckDistance,groundLayer);

        /*if (onWall && onGround)
        {
            ChangeGravity();
        }*/
        

        if (rb.gravityScale != playerRb.gravityScale)
        {
            readyToChange = true;
        }
        else
        {
            readyToChange = false;
            changeDelayCounter = 0f;
        }

        if (rb.gravityScale == 0)
        {
            rb.gravityScale = 10;
        }
    }

    private void ChargePlayer()
    {
        Vector3 localPosition = player.transform.position - transform.position;
        localPosition = localPosition.normalized;
        if (player.position.x <= transform.position.x)
        {
            transform.Translate(localPosition.x * Time.deltaTime * 10,localPosition.y * Time.deltaTime * 10,localPosition.z * Time.deltaTime * 10);
            rb.gravityScale = 0;
        }
        else
        {
            AllCheck();
        }
    }

    private void OnDrawGizmos()
    {
        Debug.DrawRay(transform.position,Vector3.down * groundCheckDistance,Color.red);
        Debug.DrawRay(transform.position,Vector3.up * groundCheckDistance,Color.red);
        
        Debug.DrawRay(checkTop.position ,Vector3.right * frontCheckDistance,Color.green) ;
        Debug.DrawRay(checkUnder.position ,Vector3.right * frontCheckDistance,Color.green);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.CompareTag("Player"))
        {
            //method to Game Over
            Debug.Log("Game Over");
        }
    }
}
