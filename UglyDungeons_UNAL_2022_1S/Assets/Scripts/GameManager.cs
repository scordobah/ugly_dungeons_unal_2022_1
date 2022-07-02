using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField]
    private Transform _player;
  
    private void Awake() 
    { 
        // If there is an instance, and it's not me, delete myself.
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        } 
    }

    public void StartGame()
    {
        GameEvents.OnStartGameEvent();
    }

    public void ReturnToMenu()
    {
        GameEvents.OnStartScreenEvent?.Invoke();
    }

    public void PausedGame()
    {
        GameEvents.OnGamePausedEvent();
    }
}
