using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject _pauseMenu;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            PauseGame();
        }
    }
    
    public void LoadMenu()
    {
        Time.timeScale = 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public void PauseGame()
    {
        Time.timeScale = _pauseMenu.activeSelf ? 1 : 0;
        _pauseMenu.SetActive(!_pauseMenu.activeSelf);
    }
}
