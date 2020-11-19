using UnityEngine;
using System.Collections;

//This is patrol state of enemy whitch implements IEnemyAI
//Current state is default state, and do not require
//any condition to be in this state


public class PatrolState : IEnemyAI
{

    EnemyStates enemy;
    int nextWayPoint = 0;

    public PatrolState(EnemyStates enemy)
    {
        this.enemy = enemy;
    }

    public void UpdateActions()
    {
        Watch();
        Patrol();
    }

    void Watch()
    {
        if (enemy.EnemySpotted())
        {
            ToChaseState();
        }
    }

    void Patrol()
    {
        if (enemy.waypoints.Length != 0)
        {
            enemy.navMeshAgent.destination = enemy.waypoints[nextWayPoint].position;
            enemy.navMeshAgent.isStopped = false;
            GoToNextPoint();
        }
    }

    void GoToNextPoint()
    {
        if (enemy.navMeshAgent.remainingDistance <= enemy.navMeshAgent.stoppingDistance
            && !enemy.navMeshAgent.pathPending)
        {
            nextWayPoint = (nextWayPoint + 1) % enemy.waypoints.Length;
        }
    }

    public void OnTriggerEnter(Collider enemy)
    {
        if (enemy.gameObject.CompareTag("Player"))
        {
            ToAlertState();
        }
    }

    public void ToPatrolState()
    {
        //You cannot go from PatrolState stright to PatrolState
        //This rule is set by me, you can change it at will
    }

    public void ToAttackState()
    {
        enemy.currentState = enemy.attackState;
    }

    public void ToAlertState()
    {
        enemy.currentState = enemy.alertState;
    }

    public void ToChaseState()
    {
        enemy.currentState = enemy.chaseState;
    }

}