using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    private AiState _currentState;
    public AiState CurrentState => _currentState;
    private Enemy _enemy;

    public StateMachine(AiState initState, Enemy enemy)
    {
        this._enemy = enemy;
        this._currentState = initState;
        ChangeState(initState);
    }

    private void ChangeState(AiState newState)
    {
        if (_currentState != null)
        {
            _currentState.OnExits(_enemy);
        }
        _currentState = newState;
        _currentState.OnEnter(_enemy);
    }

}
