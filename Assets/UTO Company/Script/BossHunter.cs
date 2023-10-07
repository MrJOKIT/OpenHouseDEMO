using System;
using System.Collections;
using System.Collections.Generic;
using Com.LuisPedroFonseca.ProCamera2D;
using UnityEngine;

public class BossHunter : MonoBehaviour
{
    [SerializeField] private float timeToBack;
    private float timeToBackCounter;
    [SerializeField] private Transform player;
    [SerializeField] private Transform onPos;
    [SerializeField] private Transform outPos;
    
    [SerializeField] private float changeDelay;
    private float changeDelayCounter;
    
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private float frontCheckDistance;
    public LayerMask groundLayer;
    public LayerMask wallLayer;
    public Transform checkTop;
    public Transform checkUnder;
    private bool onGround;
    private bool onWall;
    private bool readyToChange;
    

    private Rigidbody2D rb;
    private Rigidbody2D playerRb;
    private PlayerController _playerController;
    private SpriteRenderer spriteRenderer;
    private GameController _gameController;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerRb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        _playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        _gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    private void Update()
    {
        AllCheck();
        ChargePlayer();
        CheckRunPosition();

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
        onGround = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, groundLayer);
        onWall = Physics2D.Raycast(checkUnder.position, Vector2.right, frontCheckDistance,wallLayer);

        if (onWall)
        {
            ChangeGravity();
        }
        

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
    
    private void CheckRunPosition()
    {
        if (transform.position.x < onPos.position.x  && transform.position.x > outPos.position.x && !_playerController.onPower)
        {
            timeToBackCounter += Time.deltaTime;
            if (timeToBackCounter > timeToBack )
            {
                transform.position += Vector3.right * Time.deltaTime;
                if (transform.position.x > onPos.position.x)
                {
                    transform.position = new Vector3(onPos.position.x, transform.position.y);
                    timeToBackCounter = 0f;
                }
            }
        }
        
        if (transform.position.x < outPos.position.x && !_playerController.onPower)
        {
            timeToBackCounter += Time.deltaTime;
            if (timeToBackCounter > timeToBack )
            {
                transform.position = new Vector3(outPos.position.x + 3, outPos.position.y);
                timeToBackCounter = 0f;
            }
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

    public void PlayerOnPower()
    {
        transform.position = Vector3.Lerp(transform.position,outPos.position, 10 * Time.deltaTime);
    }
    

    private void OnDrawGizmos()
    {
        Debug.DrawRay(transform.position,Vector3.down * groundCheckDistance,Color.red);
        
        Debug.DrawRay(checkUnder.position ,Vector3.right * frontCheckDistance,Color.green);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.CompareTag("Player"))
        {
            _gameController.GameOver();
            Debug.Log("Game Over");
        }
    }
    
    
}
