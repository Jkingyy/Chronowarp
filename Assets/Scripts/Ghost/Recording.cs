using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recording : MonoBehaviour
{

    
    //list of inputs and movements for each frame
    public List<Dictionary<string,bool>> _frameRecording = new List<Dictionary<string, bool>>();
    public List<Vector2> _playerPositions = new List<Vector2>();
    public List<Vector2> _movementInputs = new List<Vector2>();
    
    [SerializeField] GameObject _ghostPrefab;
    
    private bool _isRecording;
    
    /// <summary>
    /// ////////////////COMPONENT REFERENCES////////////////////////
    /// </summary>
    private PlayerMovement _playerMovement;

    void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
    }

    // Start is called before the first frame update
    void Start()
    {
        StartRecording();
    }

    // Update is called once per frame
    void Update()
    {
        if (_isRecording)
        {
            RecordFrame();
        }
    }

    void RecordFrame()
    {

        GetInputs();
        GetPosition();
    }

    void GetInputs()
    {
        Dictionary<string, bool> _frameInputs = new Dictionary<string, bool>();
        AddInputToDictionary("HasDashed", _playerMovement._hasDashed, _frameInputs);
        AddInputToDictionary("IsDashing", _playerMovement._isDashing, _frameInputs);
        AddInputToDictionary("Sprint", _playerMovement._isSprinting, _frameInputs);
        
        AddFrameInputsToList(_frameInputs);
    }

    void AddInputToDictionary(string action, bool isPressed, Dictionary<string,bool> Dictionary)
    {
        Dictionary.Add(action, isPressed);
    }
    
    void AddFrameInputsToList( Dictionary<string,bool> Dictionary)
    {
        _frameRecording.Add(Dictionary);
        
    }
    
    
    void GetPosition()
    {
        _playerPositions.Add(transform.position);
        _movementInputs.Add(_playerMovement._movementInput);
    }
    
    public void StartRecording()
    {
        _isRecording = true;
    }
    
    public void StopRecording()
    {
        _isRecording = false;
        PlayRecording();
    }
    
    public void PlayRecording()
    {
        Playback _Playback = Instantiate(_ghostPrefab).GetComponent<Playback>();
        
        _Playback._frameRecording = new List<Dictionary<string, bool>>(_frameRecording);
        _Playback._playerPositions = new List<Vector2>(_playerPositions); 
        _Playback._movementInputs = new List<Vector2>(_movementInputs); 
        
        _frameRecording.Clear();
        _playerPositions.Clear();
        _movementInputs.Clear();
        
    }
}
