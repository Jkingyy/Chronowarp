using System;
using System.Collections;
using System.Collections.Generic;
using Ghost;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class PlayerMovement : MonoBehaviour
{
    /// <summary>
    /// ////////////////MOVEMENT SERIALIZED////////////////////////
    /// </summary>
    
    [Header("Movement Speeds")]
    [SerializeField] private float currentSpeed = 5f;
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float sprintSpeed = 8f;
    
    [SerializeField] private float cutsceneSpeed = 2f;
    
    [Header("Dash Variables")]
    [SerializeField] private float dashSpeed = 10f;
    [SerializeField] private float dashLength = .5f, dashCooldown = 1f;
    
    /// <summary>
    /// ////////////////MOVEMENT PRIVATE////////////////////////
    /// </summary>

    //movement input
    public  Vector2 movementInput;
    
    [SerializeField] private ParticleSystem dust;  
    [SerializeField] private ParticleSystem loopingDust;  
    
    //dash timers
    private float _dashCounter;
    private float _dashCooldownCounter;
    
    /// <summary>
    /// ////////////////SHOOTING////////////////////////
    /// </summary>
    
    private Vector2 _direction = Vector2.up;
    [Header("Shooting")]
    [SerializeField] private GameObject bulletPrefab;


    /// <summary>
    /// ////////////////Events////////////////////////
    /// </summary>
    public UnityEvent OnLoop;
    public UnityEvent OnLevelFinish;

    /// <summary>
    /// ////////////////BOOL CHECKS////////////////////////
    /// </summary>
    public bool isDashing { get; private set; }
    public bool hasDashed { get; private set; }
    public bool isSprinting { get; private set; }
    bool canMove = true;
    
    const string PLAYER_LOAD = "LoadAfterLoop";
    
    /// <summary>
    /// ////////////////COMPONENT REFERENCES////////////////////////
    /// </summary>
    private Rigidbody2D _rb;
    private Animator _animator;
    private Recording _recording;
    private PlayerResources _playerResources;
    [SerializeField] PlayerStats playerStats;
    /// <summary>
    /// ////////////////Scene REFERENCES////////////////////////
    /// </summary>
    
    void Awake()
    {
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
        _recording = GetComponent<Recording>();
        _playerResources = GetComponent<PlayerResources>();
    }
    // Start is called before the first frame update
    void Start()
    {
        DisablePlayerMovement();
        _animator.Play(PLAYER_LOAD);
        currentSpeed = walkSpeed;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(canMove)
        {
            GetInputs();
            GetPlayerDirection();
            MovePlayer();
            DashTimers();
            Animate();
            if (Input.GetButtonDown("Dash"))
            {
                Dash();
            }

            if (Input.GetButtonDown("Fire1"))
            {
                Shoot();
            }


            if (Input.GetButtonDown("Loop"))
            {
                _playerResources.Damage(1);
                if (playerStats.currentHealth > 0)
                {
                    OnLoop.Invoke();
                    _animator.SetTrigger("Loop");
                }

            }
        }



    }

    void GetInputs()
    {
        movementInput.x = Input.GetAxisRaw("Horizontal");
        movementInput.y = Input.GetAxisRaw("Vertical");


            if (Input.GetButtonDown("Sprint") && !isDashing)
            {
                isSprinting = true;
                if (!isDashing)
                {
                    currentSpeed = sprintSpeed;
                }
                CreateParticle(loopingDust);
            }
            if (Input.GetButtonUp("Sprint"))
            {
                isSprinting = false;
                if (!isDashing)
                {
                    currentSpeed = walkSpeed;
                }
                StopParticle(loopingDust);
            }


        
        movementInput.Normalize();
    }
    
    void MovePlayer()
    {
        _rb.velocity = movementInput * currentSpeed;
    }

    void Dash()
    {
        if (!(_dashCooldownCounter <= 0) || !(_dashCounter <= 0)) return;
        
        //do the dash
        hasDashed = true;
        isDashing = true;
        currentSpeed = dashSpeed;
        _dashCounter = dashLength;
        CreateParticle(dust);

    }

    void DashTimers()
    {
        if (_dashCounter > 0)
        {
            //subtract time from the length of the dash
            _dashCounter -= Time.deltaTime;
            
            
            //if the dash time has run out
            if (_dashCounter <= 0)
            {
                //dash has ended
                isDashing = false;
                currentSpeed = walkSpeed;
                _dashCooldownCounter = dashCooldown;
            }
        }

        
        //if cooldown is not finished
        if (_dashCooldownCounter > 0)
        {
            //subtract from the time
            _dashCooldownCounter -= Time.deltaTime;
        }
    }
    // ReSharper disable Unity.PerformanceAnalysis
    void Shoot() 
    {
        if(!Input.GetButtonDown("Fire1")) return;
        
        
        Vector2 _BulletSpawnPoint = (Vector2)transform.position + _direction;
        
        
        PlayerBullet _PlayerBullet = Instantiate(bulletPrefab, _BulletSpawnPoint, Quaternion.identity).GetComponent<PlayerBullet>();
        _PlayerBullet.SetStraightVelocity(_direction);
        _animator.SetTrigger("Shoot");
    }

    void GetPlayerDirection()
    {
        if(!IsPlayerMoving()) return;
        if (IsCloserToOne(movementInput.x, movementInput.y))
        {
            //left or right
            
            switch (movementInput.x)
            {
                case > 0:
                    _direction = Vector2.right;
                    break;
                case < 0:
                    _direction = Vector2.left;
                    break;
            }
        }
        else
        {
            //up or down
            switch (movementInput.y)
            {
                case > 0:
                    _direction = Vector2.up;
                    break;
                case < 0:
                    _direction = Vector2.down;
                    break;
            }
        }
    }
    
    bool IsCloserToOne(float Chosen, float Comparison)
    {
        return Mathf.Abs(Chosen) > Mathf.Abs(Comparison);
    }


    bool IsPlayerMoving()
    {
        return movementInput != Vector2.zero;
    }

    void Animate()
    {
        if (IsPlayerMoving())
        {
            _animator.SetFloat("Horizontal", movementInput.x);
            _animator.SetFloat("Vertical", movementInput.y);
        }

        if (!isDashing)
        {
            _animator.SetFloat("Speed", movementInput.sqrMagnitude); 
        }

        if (hasDashed)
        {
            _animator.SetTrigger("Dash");
            hasDashed = false;
        }
        
        _animator.SetBool("isSprinting", isSprinting);

            
    }

    void ReloadScene()
    {
        string _CurrentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(_CurrentSceneName);
    }

    private Door _door;
    public void PlayDoorEnterAnimation(Door door)
    {
        _door = door;
       
        
        _animator.SetTrigger("EnterDoor");
    }
    
    void PlayDoorExitAnimation()
    {
        if(_door == null) return;
        DisablePlayerMovement();
        StopAllParticles();
        _door.ChangeAnimationState("PlayerEnter");
    }

    void MoveInCutscene()
    {
        _rb.velocity = Vector2.up * cutsceneSpeed;
    }
    public void DisablePlayerMovement()
    {
        canMove = false;
        _rb.velocity = Vector2.zero;
    }

    public void EnablePlayerMovement()
    {
        canMove = true;
    }

    static void CreateParticle(ParticleSystem Particle)
    {
        Particle.Play();
    }

    static void StopParticle(ParticleSystem Particle)
    {
        Particle.Stop();
    }
    
    void StopAllParticles()
    {
        StopParticle(dust);
        StopParticle(loopingDust);
    }
}
