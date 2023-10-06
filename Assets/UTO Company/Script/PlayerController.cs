using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private PlayerInput playerInput;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;

    [Header("HP Setting")] 
    [SerializeField] private Slider hpSlider;
    

    [Header("Jump Setting")]
    [SerializeField] private bool onGround;
    [SerializeField] private float groundHeight;
    [SerializeField] private LayerMask groundLayer;
    

    [Header("Run Setting")] 
    [SerializeField] private Transform vfxTrans;
    [SerializeField] private Transform vfxTransReverse;
    [SerializeField] private Transform runVfx;
    [SerializeField] private Transform runVfxReverse;
    [SerializeField] private Transform runPos;
    private bool oneTime = true;
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
    private GameController _gameController;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerInput = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody2D>();
        stageSlide = GameObject.FindGameObjectWithTag("StageSlide").GetComponent<StageSlide>();
        _gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    private void Update()
    {
        PlayerJump();
        CheckRunPosition();
        UltimatePowerCheck();
        if (stageSlide.start)
        {
            HpTimer();
        }
        
    }

    private void HpTimer()
    {
        hpSlider.value -= Time.deltaTime * 0.025f;
        if (hpSlider.value <= 0)
        {
            _gameController.GameOver();
        }
    }

    public void IncreaseHp(float hpCount)
    {
        hpSlider.value += hpCount;
    }

    private void DecreaseHp(float hpCount)
    {
        hpSlider.value -= hpCount;
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

    public void PlayerHit(Transform damageTrans,float damage)
    {
        timeToBackCounter = 0f;
        DecreaseHp(damage);
        rb.AddForce(Vector3.left * 500f);
        Debug.Log("On Hit");
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
        currentGem = 0;
    }

    private void CheckRunPosition()
    {
        if (transform.position.x < runPos.position.x || outPosition)
        {
            
            outPosition = true;
            timeToBackCounter += Time.deltaTime;
            
            if (timeToBackCounter > timeToBackPos && onGround)
            {
                
                if (oneTime)
                {
                    if (rb.gravityScale < 0)
                    {
                        Transform vfx = Instantiate(runVfxReverse, vfxTransReverse.position, Quaternion.identity);
                        Destroy(vfx.gameObject,0.2f);
                    }
                    else if (rb.gravityScale > 0)
                    {
                        Transform vfx = Instantiate(runVfx, vfxTrans.position, Quaternion.identity);
                        Destroy(vfx.gameObject,0.2f);
                    }
                    oneTime = false;
                }
                
                transform.position += Vector3.right * Time.deltaTime;
                if (transform.position.x > runPos.position.x)
                {
                    transform.position = new Vector3(runPos.position.x, transform.position.y);
                    timeToBackCounter = 0f;
                    outPosition = false;
                    oneTime = true;
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
