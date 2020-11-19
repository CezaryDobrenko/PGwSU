using UnityEngine;
using System.Collections;

//This is chase state of enemy whitch implements IEnemyAI
//Current state certain condition must be made
//to enter this state:
//1.Enemy must spot player

public class ChaseState : IEnemyAI
{

    EnemyStates enemy;

    public ChaseState(EnemyStates enemy)
    {
        this.enemy = enemy;
    }

    public void UpdateActions()
    {
        Watch();
        Chase();
    }

    void Watch()
    {
        if (!enemy.EnemySpotted())
        {
            ToAlertState();
        }
    }

    void Chase()
    {
        enemy.navMeshAgent.destination = enemy.chaseTarget.position;
        enemy.navMeshAgent.isStopped = false;
        //check conditions are meet to go into attack state (range enemy)
        if (enemy.navMeshAgent.remainingDistance <= enemy.shootRange && enemy.onlyMelee == false)
        {
            enemy.navMeshAgent.isStopped = true;
            ToAttackState();
        }
        //check conditions are meet to go into attack state (melee enemy)
        if (enemy.navMeshAgent.remainingDistance <= enemy.attackRange && enemy.onlyMelee == true)
        {
            enemy.navMeshAgent.isStopped = true;
            ToAttackState();
        }
    }

    public void OnTriggerEnter(Collider enemy)
    {
        //This game dont require any action when trigger in this state
    }

    public void ToPatrolState()
    {
        //You cannot go from ChaseState stright to PatrolState
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
        //You cannot go from ChaseState stright to ChaseState
        //This rule is set by me, you can change it at will
    }

}