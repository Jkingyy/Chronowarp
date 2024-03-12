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


    void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    public void Damage(int DamageAmount)
    {
        Debug.Log("Destructible Damaged");
        _animator.SetTrigger("Destroy");
        SoundFXManager.Instance.PlaySoundFXClip(destroySound,transform,1f);
        _button.isDestroyed();
    }
    
    public void Destroy()
    {
        Destroy(gameObject);
    }
}

