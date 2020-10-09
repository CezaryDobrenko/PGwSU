using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertState : IEnemyAI
{

    EnemyStates enemy;

    public AlertState(EnemyStates enemy)
    {
        this.enemy = enemy;
    }

    public void UpdateActions()
    {

    }

    public void OnTriggerEnter(Collision enemy)
    {

    }

    public void toPatrolState()
    {

    }

    public void ToAttackState()
    {

    }

    public void ToAlertState()
    {

    }

    public void ToChaseState()
    {

    }
}
