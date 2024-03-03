using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playback : MonoBehaviour
{
    
    //list of inputs and movements for each frame
    public List<Dictionary<string,bool>> _frameRecording = new List<Dictionary<string, bool>>();
    public List<Vector2> _playerPositions = new List<Vector2>();
    public List<Vector2> _movementInputs = new List<Vector2>();

    private Vector2 _currentMovementInput;
    private Vector2 _direction = Vector2.up;
    private int _frameCounter = 0;
    
    public Animator _animator;

    void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(_frameCounter < _frameRecording.Count)
        {
            PlayFrame();
        }

        
    }

    void PlayFrame()
    {
        transform.position = _playerPositions[_frameCounter];
        _currentMovementInput = _movementInputs[_frameCounter];
        _frameCounter++;
        
        Animate();
    }

    public void SetPositionList(List<Vector2> PlayerPositions)
    {
        _playerPositions = PlayerPositions;
    }

    public void SetInputList(List<Dictionary<string, bool>> FrameRecording)
    {
        _frameRecording = FrameRecording;
    }
    void GetGhostDirection()
    {
        if(!IsGhostMoving()) return;
        if (IsCloserToOne(_currentMovementInput.x, _currentMovementInput.y))
        {
            //left or right
            
            switch (_currentMovementInput.x)
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
            switch (_currentMovementInput.y)
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


    bool IsGhostMoving()
    {
        return _currentMovementInput != Vector2.zero;
    }
    void Animate()
    {
        if (IsGhostMoving())
        {
            _animator.SetFloat("Horizontal", _currentMovementInput.x);
            _animator.SetFloat("Vertical", _currentMovementInput.y);
        }


        _animator.SetFloat("Speed", _currentMovementInput.sqrMagnitude); 
       
        //
        // if (_hasDashed)
        // {
        //     _animator.SetTrigger("Dash");
        //     _hasDashed = false;
        // }
        //
        // _animator.SetBool("isSprinting", _isSprinting);

            
    }
}
