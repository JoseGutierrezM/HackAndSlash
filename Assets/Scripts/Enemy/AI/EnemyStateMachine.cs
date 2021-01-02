using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : StateMachine
{
    public EnemyIdleState idleState;
    public EnemyChaseState chasingState;
    public EnemyAttackState attackState;
    public EnemyDeathState deathState;

    public void SetUpStates(Enemy enemy)
    {
        idleState = new EnemyIdleState();
        chasingState = new EnemyChaseState(enemy);
        defaultState = chasingState;
        attackState = new EnemyAttackState(enemy);
        deathState = new EnemyDeathState(enemy);
    }
}