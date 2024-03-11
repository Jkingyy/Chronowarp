using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrap : MonoBehaviour, IInteractable
{
    
    Collider2D trapCollider;
    Animator _animator;
    
     bool isActive;
     
     [SerializeField] bool startActive;
    
    bool trapIsLive;
    void Awake()
    {
        _animator = GetComponent<Animator>();
        trapCollider = GetComponent<Collider2D>();
    }
    void Start()
    {
        if(startActive)
        {
            Activate();
        }
    }
    public void Activate()
    {
        _animator.SetTrigger("Activate");
        isActive = true;
    }

    public void Deactivate()
    {
        _animator.SetTrigger("Retract");
        isActive = false;
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
    
     void SetTrapIsLive()
    {
        trapIsLive = true;
    }

     void SetTrapIsOff()
    {
        trapIsLive = false;
    }
    void OnTriggerStay2D(Collider2D other)
    {
        
        if(!other.CompareTag("PlayerFeet")) return;
        if(!trapIsLive) return;
        
        
        //Damage the enemy
        IDamageable iDamageable = other.gameObject.GetComponentInParent<IDamageable>();
        if (iDamageable != null)
        {
            iDamageable.Damage(1);
        }
         
        
         
        PlayerMovement playerMovement = other.gameObject.GetComponentInParent<PlayerMovement>();
        if(playerMovement == null) return;
        
        Animator playerAnim = other.gameObject.GetComponentInParent<Animator>();
        playerAnim.SetTrigger("Loop");
        trapCollider.enabled = false;
        playerMovement.OnLoop.Invoke();

        
    }
}
