using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private GameState _gameState;
    public GameState GameState => _gameState;
    public int timeScale = 1;

    private void Start()
    {
        this.ChangeState(GameState.START);
    }

    public void ChangeState(GameState newState)
    {
        if (this._gameState != newState)
        {
            this._gameState = newState;
        }

        switch (newState)
        {
            case GameState.START:
                LevelManager.Instance.ChangeLevelState(LevelState.BUILDING);
                PlayGame();
                break;
            case GameState.PAUSE:
                PauseGame();
                break;
            case GameState.PLAY:
                PlayGame();
                break;
            default:
                return;
        }
    }

    private void PauseGame()
    {
        Time.timeScale = 0;
    }

    private void PlayGame()
    {
        Time.timeScale = timeScale;
    }
}
