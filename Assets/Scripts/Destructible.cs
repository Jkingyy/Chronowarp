using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Destructible : MonoBehaviour,IDamageable
{
    Animator _animator;
    [SerializeField] Button _button;
    [SerializeField] AudioClip destroySound; 

    Collider2D trapCollider;

    void Awake()
    {
        trapCollider = GetComponent<Collider2D>();
        _animator = GetComponent<Animator>();
    }
    public void Damage(int DamageAmount)
    {
        _animator.SetTrigger("Destroy");
        SoundFXManager.Instance.PlaySoundFXClip(destroySound,transform,1f);
        trapCollider.enabled = false;
        _button.isDestroyed();
    }
    
    public void Destroy()
    {
        Destroy(gameObject);
    }
}

