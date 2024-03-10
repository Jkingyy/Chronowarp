using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    
    [SerializeField] SettingsSO settings;
    
    public TMP_Dropdown resolutionDropdown;
    public TMP_Dropdown qualityDropdown;
    public Slider volumeSlider;
    public Toggle fullscreenToggle;
    
    Resolution[] resolutions;
    
    void Start()
    {
        
        resolutions = Screen.resolutions;
        
        resolutionDropdown.ClearOptions();
        
        List<string> options = new List<string>();
        
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);
        }
        
        resolutionDropdown.AddOptions(options);
        

        
        LoadSettings();
    }
    
    void LoadSettings()
    {
        
        SetResolution(settings.resolutionIndex);
        SetFullscreen(settings.fullScreen);
        SetVolume(settings.volume);
        SetQuality(settings.quality);
        RefreshDisplay();
    }
    
    void RefreshDisplay()
    {
        resolutionDropdown.value = settings.resolutionIndex;
        resolutionDropdown.RefreshShownValue();
        qualityDropdown.value = settings.quality;
        qualityDropdown.RefreshShownValue();
        volumeSlider.value = settings.volume;
        fullscreenToggle.isOn = settings.fullScreen;
    }
    
    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        settings.resolutionIndex = resolutionIndex;
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
        settings.volume = volume;
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        settings.quality = qualityIndex;
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        settings.fullScreen = isFullscreen;
    }
}
