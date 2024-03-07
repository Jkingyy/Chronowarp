using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New PlayerStats", menuName = "PlayerStats")]
public class PlayerStats : ScriptableObject
{
    public int maxHealth;
    public int currentHealth;
    
    public bool newLevel;
    
    public int currentLevel;
    
    
    public void SetNewLevel(bool value)
    {
        newLevel = value;
    }
}


