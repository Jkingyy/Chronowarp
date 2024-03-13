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


