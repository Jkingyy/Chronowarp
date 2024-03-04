using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HealthHeart : MonoBehaviour
{
    
    public Sprite fullHeart, emptyHeart;
    Image heartImage;
    
    const string HEART_DAMAGE = "Heart Damaged";
    const string HEART_IDLE = "Heart Idle";
    
    
    private HeartStatus currentStatus;
    private Animator _animator;
    void Awake()
    {
        heartImage = GetComponent<Image>();
        _animator = GetComponent<Animator>();
    }
    

    public void SetHeartImage(HeartStatus Status)
    {
        currentStatus = Status;
        if(Status == HeartStatus.Empty)
        {
            heartImage.sprite = emptyHeart;
        }
        else
        {
            heartImage.sprite = fullHeart;
            _animator.Play(HEART_IDLE);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage()
    {
        if(currentStatus == HeartStatus.Full)
        {
            _animator.Play(HEART_DAMAGE);
        }
    }
}

public enum HeartStatus
{
    Empty = 0,
    Full = 1
}
