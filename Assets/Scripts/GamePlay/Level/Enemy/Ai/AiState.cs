using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface AiState
{
    public void OnEnter(Enemy enemy);
    public void OnUpdate(Enemy enemy);
    public void OnFixedUpdate(Enemy enemy);
    public void OnExits(Enemy enemy);
}
