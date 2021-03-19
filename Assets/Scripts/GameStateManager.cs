using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public enum GameState
{
    Paused,
    InGame,
    GameOver
}

public abstract class GameStateManager: MonoSingleton<GameManager>
{
    public GameState CurrentGameState { get; protected set; }
}
