using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    
    [Header("Dash Variables")]
    [SerializeField] private float dashSpeed = 10f;
    [SerializeField] private float dashLength = .5f, dashCooldown = 1f;
    
    /// <summary>
    /// ////////////////MOVEMENT PRIVATE////////////////////////
    /// </summary>

    //movement input
    public  Vector2 _movementInput;
    
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
    /// ////////////////BOOL CHECKS////////////////////////
    /// </summary>
    public bool _isDashing;
    public bool _hasDashed;
    public bool _isSprinting;
    
    

    /// <summary>
    /// ////////////////COMPONENT REFERENCES////////////////////////
    /// </summary>
    private Rigidbody2D _rb;
    private Animator _animator;
    private Recording _recording;
    /// <summary>
    /// ////////////////Scene REFERENCES////////////////////////
    /// </summary>
    
    void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
        _recording = GetComponent<Recording>();
        
        currentSpeed = walkSpeed;
        
    }

    // Update is called once per frame
    void Update()
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
            _recording.StopRecording();
            
            string currentSceneName = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(currentSceneName);

        }



    }

    void GetInputs()
    {
        _movementInput.x = Input.GetAxisRaw("Horizontal");
        _movementInput.y = Input.GetAxisRaw("Vertical");


            if (Input.GetButtonDown("Sprint") && !_isDashing)
            {
                _isSprinting = true;
                if (!_isDashing)
                {
                    currentSpeed = sprintSpeed;
                }
                CreateParticle(loopingDust);
            }
            if (Input.GetButtonUp("Sprint"))
            {
                _isSprinting = false;
                if (!_isDashing)
                {
                    currentSpeed = walkSpeed;
                }
                StopParticle(loopingDust);
            }


        
        _movementInput.Normalize();
    }
    
    void MovePlayer()
    {
        _rb.velocity = _movementInput * currentSpeed;
    }

    void Dash()
    {
        if (!(_dashCooldownCounter <= 0) || !(_dashCounter <= 0)) return;
        
        //do the dash
        _hasDashed = true;
        _isDashing = true;
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
                _isDashing = false;
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
        if (IsCloserToOne(_movementInput.x, _movementInput.y))
        {
            //left or right
            
            switch (_movementInput.x)
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
            switch (_movementInput.y)
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
        return _movementInput != Vector2.zero;
    }

    void Animate()
    {
        if (IsPlayerMoving())
        {
            _animator.SetFloat("Horizontal", _movementInput.x);
            _animator.SetFloat("Vertical", _movementInput.y);
        }

        if (!_isDashing)
        {
            _animator.SetFloat("Speed", _movementInput.sqrMagnitude); 
        }

        if (_hasDashed)
        {
            _animator.SetTrigger("Dash");
            _hasDashed = false;
        }
        
        _animator.SetBool("isSprinting", _isSprinting);

            
    }
    
    void CreateParticle(ParticleSystem Particle)
    {
        Particle.Play();
    }

    void StopParticle(ParticleSystem Particle)
    {
        Particle.Stop();
    }
}
