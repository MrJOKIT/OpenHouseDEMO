using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerInput playerInput;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;

    [Header("Jump Setting")]
    [SerializeField] private bool onGround;
    [SerializeField] private float groundHeight;
    [SerializeField] private LayerMask groundLayer;

    [Header("Run Setting")] 
    [SerializeField] private Transform runPos;
    private bool outPosition;
    [SerializeField] private float timeToBackPos;
    private float timeToBackCounter;

    [Header("Dead Setting")] 
    //[SerializeField] private bool canDead;
    //[SerializeField] private float canHitTime;
    //private bool canHit = true;
    //private float canHitTimeCounter;

    [Header("Power setting")] 
    [SerializeField] private float onPowerTime;
    private float onPowerTimeCounter;
    [SerializeField] private int maxCollectGem;
    public int currentGem;
    private bool onPower;
    
    [Header("Ref")] 
    private Animator animator;
    private StageSlide stageSlide;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerInput = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody2D>();
        stageSlide = GameObject.FindGameObjectWithTag("StageSlide").GetComponent<StageSlide>();
    }

    private void Update()
    {
        PlayerJump();
        CheckRunPosition();
        PlayerHit();
        UltimatePowerCheck();
    }

    private void PlayerJump()
    {
        onGround = Physics2D.Raycast(transform.position, Vector2.up, groundHeight, groundLayer) || Physics2D.Raycast(transform.position, Vector2.down, groundHeight, groundLayer);;
        
        if (playerInput.actions["Jump"].triggered && onGround)
        {
            rb.gravityScale *= -1;
        }

        if (rb.gravityScale < 0)
        {
            spriteRenderer.flipY = true;
            
        }
        else if (rb.gravityScale > 0)
        {
            spriteRenderer.flipY = false;
            
        }
    }

    public void PlayerHit()
    {
        
    }

    private void UltimatePowerCheck()
    {
        if (currentGem >= maxCollectGem)
        {
            onPower = true;
        }
        if (onPower)
        {
            StartUltimatePower();
            onPowerTimeCounter += Time.deltaTime;
            if (onPowerTimeCounter >= onPowerTime)
            {
                CancelUltimatePower();
                onPower = false;
                onPowerTimeCounter = 0f;
            }
        }
    }

    private void StartUltimatePower()
    {
        
    }

    private void CancelUltimatePower()
    {
        
    }

    private void CheckRunPosition()
    {
        if (transform.position.x < runPos.position.x || outPosition)
        {
            outPosition = true;
            timeToBackCounter += Time.deltaTime;
            if (timeToBackCounter > timeToBackPos && onGround)
            {
                transform.position += Vector3.right * Time.deltaTime;
                if (transform.position.x > runPos.position.x)
                {
                    transform.position = new Vector3(runPos.position.x, transform.position.y);
                    timeToBackCounter = 0f;
                    outPosition = false;
                }
            }
        }
        
        
    }

    private void OnDrawGizmos()
    {
        Debug.DrawRay(transform.position,Vector3.up * groundHeight);
        Debug.DrawRay(transform.position,Vector3.down * groundHeight);
    }
    
}
