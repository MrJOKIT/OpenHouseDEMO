using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private PlayerInput playerInput;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;

    [Header("HP Setting")] 
    public AudioClip hitSound;
    [SerializeField] private float decreaseHpSpeed = 0.025f;
    [SerializeField] private Slider hpSlider;
    [SerializeField] private Image sliderImage;
    [SerializeField] private Material ultimateMat;
    private bool hpCounter = true;
    

    [Header("Jump Setting")]
    [SerializeField] private bool onGround;
    [SerializeField] private float groundHeight;
    [SerializeField] private LayerMask groundLayer;


    [Header("Run Setting")] 
    [SerializeField] private Transform walkVfxReverse;
    [SerializeField] private Transform walkVfx;
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
    public List<Image> boneUIImage;
    [SerializeField] private float onPowerTime;
    private float onPowerTimeCounter;
    [SerializeField] private int maxCollectGem;
    public int currentGem;
    public bool onPower;
    [Header("Point Taker Setting")] 
    [SerializeField] private float takerRadius;
    [SerializeField] private LayerMask pointLayer;
    [SerializeField] private LayerMask lifeGemLayer;
    [SerializeField] private LayerMask ultiGemLayer;
    
    
    [Header("Ref")] 
    private Animator animator;
    private StageSlide stageSlide;
    private GameController _gameController;
    private BossHunter _bossHunter;
    

    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerInput = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody2D>();
        stageSlide = GameObject.FindGameObjectWithTag("StageSlide").GetComponent<StageSlide>();
        _gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        _bossHunter = GameObject.FindGameObjectWithTag("Boss").GetComponent<BossHunter>();
    }

    private void Update()
    {
        
        if (stageSlide.start && hpCounter)
        {
            HpTimer();
        }

        if (!_gameController.gameOver)
        {
            PointTaker();
            PlayerJump();
            CheckRunPosition();
            UltimatePowerCheck();
        }
        
    }

    private void HpTimer()
    {
        hpSlider.value -= Time.deltaTime * decreaseHpSpeed;
        if (hpSlider.value <= 0)
        {
            _gameController.GameOver();
        }
    }

    public void IncreaseHp(float hpCount)
    {
        //decreaseHpSpeed *= 1.25f;
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
            SmokeEffect();
            animator.SetTrigger("Jump");
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
        SoundEffectPlayer.instance.PlayerAudio(hitSound);
        animator.SetTrigger("Hurt");
        timeToBackCounter = 0f;
        DecreaseHp(damage);
        rb.AddForce(Vector3.left * 500f);
        
        Debug.Log("On Hit");
    }

    private void UltimatePowerCheck()
    {
        if (currentGem == 1)
        {
            boneUIImage[0].gameObject.SetActive(true);
            boneUIImage[1].gameObject.SetActive(false);
            boneUIImage[2].gameObject.SetActive(false);
            boneUIImage[3].gameObject.SetActive(false);
            boneUIImage[4].gameObject.SetActive(false);
        }
        else if (currentGem == 2)
        {
            boneUIImage[0].gameObject.SetActive(true);
            boneUIImage[1].gameObject.SetActive(true);
            boneUIImage[2].gameObject.SetActive(false);
            boneUIImage[3].gameObject.SetActive(false);
            boneUIImage[4].gameObject.SetActive(false);
        }
        else if (currentGem == 3)
        {
            boneUIImage[0].gameObject.SetActive(true);
            boneUIImage[1].gameObject.SetActive(true);
            boneUIImage[2].gameObject.SetActive(true);
            boneUIImage[3].gameObject.SetActive(false);
            boneUIImage[4].gameObject.SetActive(false);
        }
        else if (currentGem == 4)
        {
            boneUIImage[0].gameObject.SetActive(true);
            boneUIImage[1].gameObject.SetActive(true);
            boneUIImage[2].gameObject.SetActive(true);
            boneUIImage[3].gameObject.SetActive(true);
            boneUIImage[4].gameObject.SetActive(false);
        }
        else if (currentGem >= maxCollectGem)
        {
            boneUIImage[0].gameObject.SetActive(true);
            boneUIImage[1].gameObject.SetActive(true);
            boneUIImage[2].gameObject.SetActive(true);
            boneUIImage[3].gameObject.SetActive(true);
            boneUIImage[4].gameObject.SetActive(true);
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

    public void GetUltimatePoint()
    {
        currentGem += 1;
    }

    private void StartUltimatePower()
    {
        //transform.gameObject.tag = "Boss";
        takerRadius = 10;
        _bossHunter.PlayerOnPower();
        stageSlide.SlideSpeed = 20;
        sliderImage.color = Color.gray;
        sliderImage.material = ultimateMat;
        hpCounter = false;
    }

    private void CancelUltimatePower()
    {
        //transform.gameObject.tag = "Player";
        takerRadius = 1;
        stageSlide.SlideSpeed = 5;
        for (int i = 0; i < boneUIImage.Count; i++)
        {
            boneUIImage[i].gameObject.SetActive(false);
        }

        sliderImage.color = Color.cyan;
        sliderImage.material = null;
        hpCounter = true;
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

    public void ResetPosition()
    {
        transform.position = runPos.position;
    }

    private void PointTaker()
    {
        Collider2D[] point = Physics2D.OverlapCircleAll(transform.position, takerRadius, pointLayer);

        foreach (Collider2D mail in point)
        {
            mail.GetComponent<MailPoint>().onTake = true;
        }

        Collider2D[] ultiGem = Physics2D.OverlapCircleAll(transform.position, takerRadius, ultiGemLayer);
        
        foreach (var ulti in ultiGem)
        {
            ulti.GetComponent<BoneGem>().onTake = true;
        }

        Collider2D[] lifeGem = Physics2D.OverlapCircleAll(transform.position, takerRadius, lifeGemLayer);
        
        foreach (var life in lifeGem)
        {
            life.GetComponent<LifeGem>().onTake = true;
        }
    }

    private void OnDrawGizmos()
    {
        Debug.DrawRay(transform.position,Vector3.up * groundHeight);
        Debug.DrawRay(transform.position,Vector3.down * groundHeight);
        
        Gizmos.DrawWireSphere(transform.position,takerRadius);
    }

    public void SmokeEffect()
    {
        
        
        if (rb.gravityScale < 0)
        {
            Transform vfx = Instantiate(walkVfxReverse, vfxTransReverse.position, Quaternion.identity);
            Destroy(vfx.gameObject,0.2f);
        }
        else if (rb.gravityScale > 0)
        {
            Transform vfx = Instantiate(walkVfx, vfxTrans.position, Quaternion.identity);
            Destroy(vfx.gameObject,0.2f);
        }
    }
}
