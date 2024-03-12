using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BearTrap : MonoBehaviour, IInteractable
{
    Collider2D trapCollider;
    Animator _animator;
    
    [SerializeField] AudioClip bearTrapSound;
    
    private bool _isActive;
    
    void Awake()
    {
        _animator = GetComponent<Animator>();
        trapCollider = GetComponent<Collider2D>();
    }

    public void Activate()
    {
        _animator.SetTrigger("Activate");
        SoundFXManager.Instance.PlaySoundFXClip(bearTrapSound,transform,1f);
    }

    public void Deactivate()
    {
        trapCollider.enabled = false;
    }

    public void ChangeState()
    {
        
    }
    
    
    void OnTriggerEnter2D(Collider2D other)
    {
        
        if(!other.CompareTag("PlayerFeet")) return;
        
        Activate();
        
        //Damage the enemy
         IDamageable iDamageable = other.gameObject.GetComponentInParent<IDamageable>();
         if (iDamageable != null)
         {
             iDamageable.Damage(1);
         }
         
         Deactivate();
         
         PlayerMovement playerMovement = other.gameObject.GetComponentInParent<PlayerMovement>();
         if(playerMovement == null) return;
         
         Animator playerAnim = other.gameObject.GetComponentInParent<Animator>();
         playerAnim.SetTrigger("Loop");
         
         playerMovement.OnLoop.Invoke();

        
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
