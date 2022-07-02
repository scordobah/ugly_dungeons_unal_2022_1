using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIGameController : MonoBehaviour
{
    [SerializeField] 
    private GameObject _startScreen;
    [SerializeField] 
    private GameObject _gameScreen;
    [SerializeField] 
    private GameObject _pauseScreen;
    [SerializeField] 
    private GameObject _endScreen;

    void Start()
    {
        GameEvents.OnStartScreenEvent += OnStartScreen;
        GameEvents.OnStartGameEvent += OnStartGame;
        GameEvents.OnGamePausedEvent += OnGamePaused;
        GameEvents.OnGameOverEvent += OnGameOver;
    }

    private void OnDestroy()
    {
        GameEvents.OnStartScreenEvent -= OnStartScreen;
        GameEvents.OnStartGameEvent -= OnStartGame;
        GameEvents.OnGamePausedEvent -= OnGamePaused;
        GameEvents.OnGameOverEvent -= OnGameOver;
    }

    private void OnStartScreen()
    {
        _startScreen.SetActive(true);
        _gameScreen.SetActive(false);
        _pauseScreen.SetActive(false);
        _endScreen.SetActive(false);
        Time.timeScale = 0;
    }

    private void OnStartGame()
    {
        _startScreen.SetActive(false);
        _gameScreen.SetActive(true);
        _pauseScreen.SetActive(false);
        _endScreen.SetActive(false);
    }

    private void OnGamePaused()
    {
        _startScreen.SetActive(false);
        _gameScreen.SetActive(false);
        _pauseScreen.SetActive(true);
        _endScreen.SetActive(false);
    }
    
    private void OnGameOver()
    {
        _startScreen.SetActive(false);
        _gameScreen.SetActive(false);
        _pauseScreen.SetActive(false);
        _endScreen.SetActive(true);
    }

    public void ButtonStartGame()
    {
        GameManager.Instance.StartGame();
        Time.timeScale = 1;
    }

    public void ButtonStopGame()
    {
        GameManager.Instance.PausedGame();
        Time.timeScale = 0;
    }

    public void ButtonResumeGame()
    {
        GameManager.Instance.StartGame();
        Time.timeScale = 1;
    }

    public void ButtonQuitGame()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
