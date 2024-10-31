using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GameState _gameState;

    private void Start() {
      this.ChangeState(GameState.START);
    }

    private void ChangeState(GameState newState)
    {
      if(this._gameState != newState)
      {
        this._gameState = newState;
      }

      switch(newState)
      {
        case GameState.START:
          break;
        case GameState.WIN:
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
}
