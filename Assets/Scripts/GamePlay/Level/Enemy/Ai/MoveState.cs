using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : AiState
{
    Vector3 positionMove;
    public void OnEnter(Enemy enemy)
    {

    }

    public void OnExits(Enemy enemy)
    {

    }

    public void OnFixedUpdate(Enemy enemy)
    {

        Vector3 direction = (positionMove - enemy.transform.position).normalized;
        Rotation(enemy, direction);
        enemy.EnemyRB.velocity = direction * enemy.Speed;
    }

    public void OnUpdate(Enemy enemy)
    {
        if (enemy.PathIndex >= enemy.Agent.Count) return;
        positionMove = enemy.Agent[enemy.PathIndex].GetWorldPosition();
        float distance = Vector3.Distance(enemy.transform.position, positionMove);
        if (distance <= 0.1f)
        {
            enemy.NextIndex();
        }
    }

    private void Rotation(Enemy enemy, Vector3 direction)
    {
        Quaternion quaternion = Quaternion.LookRotation(direction);
        enemy.transform.rotation = quaternion;
    }
}
