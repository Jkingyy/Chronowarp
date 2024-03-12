using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    
    
    [SerializeField] List<LevelButton> levelButtons = new List<LevelButton>();
    
    [SerializeField] PlayerStats playerStats;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    

    public void FillLevelSelect()
    {
        foreach (LevelButton level in levelButtons)
        {
            level.UpdateDisplay();
        }
    }

    public void PlayButton()
    {
        for(int i = 0; i < levelButtons.Count; i++)
        {
            if(levelButtons[i].level.isCurrentLevel)
            {
                playerStats.ResetHealth();
                playerStats.SetNewLevel(true);
                SceneManager.LoadScene(i + 1);
            }
        }
    }
    
    [SerializeField] AudioClip hoverSound;
    [SerializeField] AudioClip clickSound;
    [SerializeField] AudioClip backSound;

    public void PlayBackSound()
    {
        SoundFXManager.Instance.PlaySoundFXClip(backSound,transform,1f);
    }
    
    public void PlayHoverSound()
    {
        SoundFXManager.Instance.PlaySoundFXClip(hoverSound,transform,1f);
    }
    
    public void PlayClickSound()
    {
        SoundFXManager.Instance.PlaySoundFXClip(clickSound,transform,1f);
    }
    
    public void QuitButton()
    {
        Application.Quit();
    }
    
}
