using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathState : State
{
    private Enemy enemy;

    public EnemyDeathState(Enemy _enemy)
    {
        enemy = _enemy;
    }

    void State.Enter()
    {
        throw new System.NotImplementedException();
    }

    void State.Execute()
    {
        throw new System.NotImplementedException();
    }

    void State.Exit()
    {
        throw new System.NotImplementedException();
    }
}
