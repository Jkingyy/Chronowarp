using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recording : MonoBehaviour
{
    //list of inputs in the current frame
    public Dictionary<string, bool> _frameInputs = new Dictionary<string, bool>();
    
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
        AddInputToDictionary("Dash", _playerMovement._hasDashed);
        AddInputToDictionary("Sprint", _playerMovement._isSprinting);
        AddFrameInputsToList();
    }

    void AddInputToDictionary(string action, bool isPressed)
    {
        _frameInputs.Add(action, isPressed);
    }
    
    void AddFrameInputsToList()
    {
        _frameRecording.Add(_frameInputs);
        _frameInputs.Clear();
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
    }
    
    public void PlayRecording()
    {
        Playback _Playback = Instantiate(_ghostPrefab).GetComponent<Playback>();
        
        _Playback._frameRecording = _frameRecording;
        _Playback._playerPositions = _playerPositions;
        _Playback._movementInputs = _movementInputs;
    }
}
