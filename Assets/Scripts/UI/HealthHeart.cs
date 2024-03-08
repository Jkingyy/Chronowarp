using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HealthHeart : MonoBehaviour
{
    
    public Sprite fullHeart, emptyHeart;
    Image _heartImage;
    
    const string HEART_DAMAGE = "Heart Damaged";
    const string HEART_IDLE = "Heart Idle";
    
    
    private HeartStatus _currentStatus;
    private Animator _animator;

    
    void Awake()
    {
        _heartImage = GetComponent<Image>();
        _animator = GetComponent<Animator>();

    }
    

    public void SetHeartImage(HeartStatus Status)
    {
        _currentStatus = Status;
        if(Status == HeartStatus.Empty)
        {
            _heartImage.sprite = emptyHeart;
        }
        else
        {
            _heartImage.sprite = fullHeart;
            _animator.Play(HEART_IDLE);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage()
    {
        if(_currentStatus == HeartStatus.Full)
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
