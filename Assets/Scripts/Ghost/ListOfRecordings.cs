using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "New List of Recordings", menuName = "Recording List" )]
public class ListOfRecordings : ScriptableObject
{

    public List<Recording> recordingList = new List<Recording>();

    
    public void AddRecording(Recording recording)
    {
        recordingList.Add(recording);
    }
}
