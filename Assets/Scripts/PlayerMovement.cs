using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float currentSpeed = 5f;
    
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float sprintSpeed = 8f;
    [SerializeField] private float dashSpeed = 10f;
    
    [SerializeField] private float dashLength = .5f, dashCooldown = 1f;
    private bool _isDashing;
    private bool _isSprinting;
    
    private float _dashCounter;
    private float _dashCooldownCounter;
    
    private Vector2 _movementInput;
    
    
    /// <summary>
    /// ////////////////ANIMATION STUFF////////////////////////
    /// </summary>
    
    private string currentState;
    
    //states
    const string PLAYER_WALK_UP = "Walk Up";
    const string PLAYER_WALK_DOWN = "Walk Down";
    const string PLAYER_WALK_LEFT = "Walk Left";
    const string PLAYER_WALK_RIGHT = "Walk Right";
    
    const string PLAYER_SPRINT_FORWARDS = "PlayerSprintForwards";
    const string PLAYER_RUN_BACKWARDS = "PlayerRunBackwards";
    const string PLAYER_SPRINT_BACKWARDS = "PlayerSprintBackwards";
    const string PLAYER_IDLE = "PlayerIdle";
    const string PLAYER_JUMP = "PlayerJump";
    const string PLAYER_RISE = "PlayerRising";
    const string PLAYER_FALL = "PlayerFalling";
    
    
    /// <summary>
    /// ////////////////COMPONENT REFERENCES////////////////////////
    /// </summary>
    private Rigidbody2D _rb;
    private Animator _animator;
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
        currentSpeed = walkSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        GetInputs();
        MovePlayer();
        DashTimers();
        
        if (Input.GetButtonDown("Dash"))
        {
            Dash();
        }
    }


    void GetInputs()
    {
        _movementInput.x = Input.GetAxisRaw("Horizontal");
        _movementInput.y = Input.GetAxisRaw("Vertical");

        if (!_isDashing)
        {
            if (Input.GetButtonDown("Sprint"))
            {
                _isSprinting = true;
                currentSpeed = sprintSpeed;
            }
            if (Input.GetButtonUp("Sprint"))
            {
                _isSprinting = false;
                currentSpeed = walkSpeed;
            }
        }

        
        _movementInput.Normalize();
    }

    void MovePlayer()
    {
        _rb.velocity = _movementInput * currentSpeed;
    }

    void Dash()
    {

        if (_dashCooldownCounter <= 0 && _dashCounter <= 0)
        {
            //do the dash
            _isDashing = true;
            currentSpeed = dashSpeed;
            _dashCounter = dashLength;
        }
        

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
    
    void ChangeAnimationState(string newState)
    {
        //stop the same animation from interrupting itself
        if(currentState == newState) return;
        
        Debug.Log(newState);
        //play the animation
        _animator.Play(newState);
        
        //reassign the current state
        currentState = newState;
    }
}
