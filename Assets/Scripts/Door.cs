using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    [SerializeField] bool  isEndDoor;
    [SerializeField] bool  startOpen;
    
    Animator _animator;
    
    bool isActive;
    
    const string DOOR_OPEN = "Open";
    const string DOOR_CLOSE = "Close";
    const string PLAYER_ENTER = "PlayerEnter";
    
    string currentState;
    
    Collider2D _collider;
    
    PlayerMovement Player;
    void Awake()
    {
        _animator = GetComponent<Animator>();
        _collider = GetComponent<Collider2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        if(startOpen)
        {
            Activate();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    
    
    
    public void ChangeAnimationState(string newState)
    {
        if(newState == currentState) return;
        
        _animator.Play(newState);
    }
    public void Activate()
    {
        ChangeAnimationState(DOOR_OPEN);
        _collider.isTrigger = true;
        isActive = true;
    }

    public void ChangeState()
    {
        if (isActive)
        {
            Deactivate();
        }
        else
        {
            Activate();
        }
    }
    
    public void Deactivate()
    {
        ChangeAnimationState(DOOR_CLOSE);
        _collider.isTrigger = false;
        isActive = false;
    }

    void LevelFinished()
    {
        Player.OnLevelFinish.Invoke();
    }
    void DisableAnimator()
    {
        _animator.enabled = false;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (isEndDoor)
        {
            if (other.CompareTag("PlayerFeet"))
            {

                Player = other.GetComponentInParent<PlayerMovement>();

                if (Player != null)
                {

                    Player.PlayDoorEnterAnimation(this); 
                }
            }
        }
    }
}
