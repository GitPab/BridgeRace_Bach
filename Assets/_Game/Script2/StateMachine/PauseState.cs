using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseState : IState
{
    public void OnEnter(Enemy enemy)
    {
        enemy.OnStopMove();
    }

    public void OnExecute(Enemy enemy)
    {
        if (!GameManager.Instance.IsState(GameState.Pause))
        {
            enemy.ChangeState(new PatrolState());
        } else
        {
            enemy.ChangeState(new IdleState());
        }
    }

    public void OnExit(Enemy enemy) { }
}
