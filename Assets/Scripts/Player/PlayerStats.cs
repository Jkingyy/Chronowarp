using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "New PlayerStats", menuName = "PlayerStats")]
public class PlayerStats : ScriptableObject
{
    public int maxHealth;
    public int currentHealth;
    
    public bool newLevel;
    
    public int currentLevel; 
    public int totalCompletedLevel;
    
    
    public void SaveData()
    {

        PlayerPrefs.SetInt("CurrentLevel", currentLevel);
        PlayerPrefs.SetInt("TotalCompletedLevel", totalCompletedLevel);
        PlayerPrefs.SetInt("NewLevel", boolToInt(newLevel));
    }
    public void LoadData()
    {
        currentLevel = PlayerPrefs.GetInt("CurrentLevel", 1);
        totalCompletedLevel = PlayerPrefs.GetInt("TotalCompletedLevel", 0);
        newLevel = intToBool(PlayerPrefs.GetInt("NewLevel",0));
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
    
    public void SetNewLevel(bool value)
    {
        newLevel = value;
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
    }
    
    public void StartNewLevel()
    {
        ResetHealth();
        newLevel = false;
    }
}


