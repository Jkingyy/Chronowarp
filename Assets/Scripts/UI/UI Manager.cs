using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject _pauseMenu;
    [SerializeField] LevelSO level;
    [SerializeField] PlayerStats playerStats;
    
    [SerializeField] GameObject _gameOverMenu;
    
    public UnityEvent SetDefaultState;
    
    private PlayerMovement _playerMovement;

    void Awake()
    {
        _playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (playerStats.totalCompletedLevel == level.levelNumber - 1)
        {
            level.isCurrentLevel = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            PauseGame();
        }
    }
    
    public void FinishedLevel()
    {
        if(playerStats.totalCompletedLevel < level.levelNumber)
        {
            playerStats.totalCompletedLevel = level.levelNumber;
        }
        level.isComplete = true;
        level.isCurrentLevel = false;
    }

    public void RestartLevel()
    {
        SetDefaultState.Invoke();
        playerStats.ResetHealth();
        playerStats.SetNewLevel(true);
        Time.timeScale = 1;
        SceneManager.LoadScene(level.levelNumber);
    }

    public void GameOver()
    {
        _gameOverMenu.SetActive(true);
        Time.timeScale = 0;
    }
    
    public void LoadMenu()
    {
        SetDefaultState.Invoke();
        Time.timeScale = 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
    
    public void ResumeGame()
    {
        _playerMovement.EnablePlayerMovement();
        Time.timeScale = _pauseMenu.activeSelf ? 1 : 0;
        _pauseMenu.SetActive(!_pauseMenu.activeSelf);
    }
    
    public void PauseGame()
    {
        _playerMovement.DisablePlayerMovement();
        if (Time.timeScale != 0)
        {
            Time.timeScale = _pauseMenu.activeSelf ? 1 : 0;
            _pauseMenu.SetActive(!_pauseMenu.activeSelf);
        }
    }
}
