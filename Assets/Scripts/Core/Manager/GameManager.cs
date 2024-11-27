using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private GameState _gameState;
    public GameState GameState => _gameState;

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
                break;
            case GameState.WIN:
                // open win panel
                ViewBase winPanel = UIManager.GetView<WinPanel>();
                if (winPanel != null)
                {
                    winPanel.Show();
                }
                PauseGame();
                break;
            case GameState.OVER:
                break;
            case GameState.PAUSE:
                break;
            case GameState.LOADING:
                break;
            default:
                return;
        }
    }

    private void PauseGame()
    {
        Time.timeScale = 0;
    }
}
