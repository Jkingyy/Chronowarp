using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Settings", menuName = "Settings")]
public class SettingsSO : ScriptableObject
{
    public int resolutionIndex;
    public bool fullScreen;
    public float masterVolume;
    public float musicVolume;
    public float soundFXVolume;
    public int quality;


    public void SaveData()
    {
       PlayerPrefs.SetFloat("MasterVolume", masterVolume);
       PlayerPrefs.SetFloat("MusicVolume", musicVolume);
       PlayerPrefs.SetFloat("SoundFXVolume", soundFXVolume);
       PlayerPrefs.SetInt("Quality", quality);
       PlayerPrefs.SetInt("Resolution", resolutionIndex);
       PlayerPrefs.SetInt("Fullscreen", boolToInt(fullScreen));
    }
    public void LoadData()
    {
        masterVolume = PlayerPrefs.GetFloat("MasterVolume", 0.5f);
        musicVolume = PlayerPrefs.GetFloat("MusicVolume", 1f);
        soundFXVolume = PlayerPrefs.GetFloat("SoundFXVolume", 1f);
        quality = PlayerPrefs.GetInt("Quality" ,5);
        resolutionIndex = PlayerPrefs.GetInt("Resolution", 17);
        fullScreen = intToBool(PlayerPrefs.GetInt("Fullscreen",1));
    }
    
    int boolToInt(bool val)
    {
        if (val)
            return 1;
        else
            return 0;
    }

    bool intToBool(int val)
    {
        if (val != 0)
            return true;
        else
            return false;
    }
}
