using System;

public static class GameEvents
{
    public static Action OnStartScreenEvent;
    public static Action OnStartGameEvent;
    public static Action OnGamePausedEvent;
    public static Action OnGameOverEvent;

    public static Action<int> OnEnemyDeathEvent;
}
