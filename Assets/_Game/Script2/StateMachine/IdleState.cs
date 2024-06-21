using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState
{
    float timer;
    float randomTime = 3;

    public void OnEnter(Enemy enemy)
    {
        enemy.ChangeAnim(AnimName.IDLE);
    }

    public void OnExecute(Enemy enemy)
    {
        timer += Time.deltaTime;

        if (timer < randomTime)
        {
            enemy.ChangeState(new PatrolState());
        }
    }
    public void OnExit(Enemy enemy)
    {

    }
}
