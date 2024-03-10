using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    public LevelSO level;
    
    [SerializeField] Image levelImage;
    [SerializeField] TextMeshProUGUI levelText;
    
    [SerializeField] Sprite uncompleteLevel;
    [SerializeField] Sprite currentLevel;
    [SerializeField] Sprite completedLevel;
    
    [SerializeField] PlayerStats playerStats;
    
    public void LoadLevel()
    {
        if(level.isComplete || level.isCurrentLevel)
        {
            playerStats.ResetHealth();
            playerStats.SetNewLevel(true);
            playerStats.currentLevel = level.levelNumber;
            SceneManager.LoadScene(level.levelNumber);
        }
    }

    public void UpdateDisplay()
    {
        UpdateNumber();
        UpdateImage();
    }

    void UpdateImage()
    {
        if(level.isComplete)
        {
            levelImage.sprite = completedLevel;
        }
        else if(level.isCurrentLevel)
        {
            levelImage.sprite = currentLevel;
        }
        else
        {
            levelImage.sprite = uncompleteLevel;
        }
    }

    void UpdateNumber()
    {
        levelText.text = level.levelNumber.ToString();
    }

}
