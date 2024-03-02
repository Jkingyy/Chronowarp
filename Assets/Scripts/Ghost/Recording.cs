using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Recording : ScriptableObject
{
    
    public List<FrameRecording> recording = new List<FrameRecording>();



    public void AddFrame(FrameRecording frame)
    {
        recording.Add(frame);
    }
}

