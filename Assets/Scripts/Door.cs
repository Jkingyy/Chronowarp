using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    
    
    Animator _animator;
    
    const string DOOR_OPEN = "Open";
    const string DOOR_CLOSE = "Close";
    const string PLAYER_ENTER = "PlayerEnter";
    
    string currentState;
    void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    
    
    
    void ChangeAnimationState(string newState)
    {
        if(newState == currentState) return;
        
        _animator.Play(newState);
    }
    public void Activate()
    {
        ChangeAnimationState(DOOR_OPEN);
    }
    public void Deactivate()
    {
        ChangeAnimationState(DOOR_CLOSE);
    }
}
