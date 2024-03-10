using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Settings", menuName = "Settings")]
public class SettingsSO : ScriptableObject
{
    public int resolutionIndex;
    public bool fullScreen;
    public float volume;
    public int quality;
}
