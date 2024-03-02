using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameRecording : ScriptableObject
{
    //list of inputs and actions for the frame
    
    //inputs that were done this frame
    List<InputRecording> inputs = new List<InputRecording>();
    
    //current position of the player in the frame
    Vector2 currentPosition;
    
    public void AddInput(InputRecording input)
    {
        inputs.Add(input);
    }
    
    public void SetPosition(Vector2 position)
    {
        currentPosition = position;
    }
}
