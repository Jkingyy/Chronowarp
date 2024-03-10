using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    
    
    [SerializeField] List<LevelButton> levelButtons = new List<LevelButton>();
    

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void StartButton()
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
                SceneManager.LoadScene(i + 1);
            }
        }
    }


    
    public void QuitButton()
    {
        Application.Quit();
    }
    
}
