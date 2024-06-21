using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairState : IState
{
    public void OnEnter(Enemy enemy)
    {

    }

    public void OnExecute(Enemy enemy)
    {
        enemy.SetDestination(enemy.winPos);
    }
    public void OnExit(Enemy enemy)
    {

    }
}
