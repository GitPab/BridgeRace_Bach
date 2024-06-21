using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IState
{
    float time = 0f;
    float counterTime = 3f;
    int rand = (int)Random.Range(0.5f, 10.5f);
    public void OnEnter(Enemy enemy)
    {
        time += Time.deltaTime;

        if (time > counterTime)
        {
            enemy.ChangeAnim(AnimName.RUN);
            enemy.SetDestination(enemy.SeekBrickSameColor());
        }
    }

    public void OnExecute(Enemy enemy)
    {

        if (enemy.brickStack.Count > rand)
        {
            enemy.ChangeState(new StairState());
        }
        else
        {
            enemy.ChangeAnim(AnimName.RUN);
            enemy.SetDestination(enemy.SeekBrickSameColor());
        }
    }

    public void OnExit(Enemy enemy)
    {
        if (GameManager.Instance.IsState(GameState.GamePlay))
        {
            if (enemy.IsDestination())
            {
                enemy.ChangeState(new PatrolState());
            }
        }
            
    }
}
