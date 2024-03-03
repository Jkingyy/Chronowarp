using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playback : MonoBehaviour
{
    
    //list of inputs and movements for each frame
    public List<Dictionary<string,bool>> _frameRecording = new List<Dictionary<string, bool>>();
    public List<Vector2> _playerPositions = new List<Vector2>();
    
    
    private int _frameCounter = 0;
    
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
        _frameCounter++;
    }

    public void SetPositionList(List<Vector2> PlayerPositions)
    {
        _playerPositions = PlayerPositions;
    }

    public void SetInputList(List<Dictionary<string, bool>> FrameRecording)
    {
        _frameRecording = FrameRecording;
    }
}
