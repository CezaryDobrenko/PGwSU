using UnityEngine;
using System.Collections;

//This is alert state of enemy whitch implements IEnemyAI
//Current state certain condition must be made
//to enter this state:
//1.Shots must be fired near enemy
//2.Enemy got shot

public class AlertState : IEnemyAI
{

    EnemyStates enemy;
    float timer = 0;

    public AlertState(EnemyStates enemy)
    {
        this.enemy = enemy;
    }

    public void UpdateActions()
    {
        Search();
        Watch();
        LookAround();
    }

    void Watch()
    {
        if (enemy.EnemySpotted())
        {
            enemy.navMeshAgent.destination = enemy.lastKnownPosition;
            ToChaseState();
        }
    }

    void LookAround()
    {
        if (enemy.navMeshAgent.remainingDistance <= enemy.navMeshAgent.stoppingDistance)
        {
            timer += Time.deltaTime;
            if (timer >= enemy.stayAlertTime)
            {
                timer = 0;
                ToPatrolState();
            }
        }
    }

    void Search()
    {
        enemy.navMeshAgent.destination = enemy.lastKnownPosition;
        enemy.navMeshAgent.isStopped = false;
    }

    public void OnTriggerEnter(Collider enemy)
    {
        //This game dont require any action when trigger in this state
    }

    public void ToPatrolState()
    {
        enemy.currentState = enemy.patrolState;
    }

    public void ToAttackState()
    {
        //You cannot go from AlertState stright to AttackState
        //This rule is set by me, you can change it at will
    }

    public void ToAlertState()
    {
        //You cannot go from AlertState to AlertState
        //This rule is set by me, you can change it at will
    }

    public void ToChaseState()
    {
        enemy.currentState = enemy.chaseState;
    }
}