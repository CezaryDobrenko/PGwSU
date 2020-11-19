using UnityEngine;
using System.Collections;

//This is attack state of enemy whitch implements IEnemyAI
//Current state certain condition must be made
//to enter this state:
//1.Enemy must spot player
//2.Enemy must be in attack range

public class AttackState : IEnemyAI
{

    EnemyStates enemy;
    float timer;

    public AttackState(EnemyStates enemy)
    {
        this.enemy = enemy;
    }

    public void UpdateActions()
    {
        float distance = Vector3.Distance(enemy.chaseTarget.transform.position, enemy.transform.position);
        timer += Time.deltaTime;

        Watch();
        Chase(distance);
        Attack(distance);

    }

    void Chase(float distance)
    {
        if (distance > enemy.attackRange && enemy.onlyMelee == true)
        {
            ToChaseState();
        }
        if (distance > enemy.shootRange && enemy.onlyMelee == false)
        {
            ToChaseState();
        }
    }

    void Attack(float distance)
    {
        //This conditions check if range enemy can attack player
        if (distance <= enemy.shootRange && distance > enemy.attackRange && enemy.onlyMelee == false && timer >= enemy.attackDelay)
        {
            Hit(true);
            timer = 0;
        }
        //This conditions check if melee enemy can attack player
        if (distance <= enemy.attackRange && enemy.onlyMelee == true && timer >= enemy.attackDelay)
        {
            Hit(false);
            timer = 0;
        }
    }

    void Hit(bool shoot)
    {
        if (shoot == true)
        {
            GameObject missile = GameObject.Instantiate(enemy.missile, enemy.transform.position, Quaternion.identity);
            missile.GetComponent<Missile>().speed = enemy.missileSpeed;
            missile.GetComponent<Missile>().damage = enemy.missileDamage;
        }
        else
            enemy.chaseTarget.SendMessage("EnemyHit", enemy.meleeDamage, SendMessageOptions.DontRequireReceiver);
    }

    void Watch()
    {
        if (!enemy.EnemySpotted())
        {
            ToAlertState();
        }
    }

    public void OnTriggerEnter(Collider enemy)
    {
        //This game dont require any action when trigger in this state
    }

    public void ToPatrolState()
    {
        //You cannot go from AttackState stright to PatrolState
        //This rule is set by me, you can change it at will
    }

    public void ToAttackState()
    {
        //You cannot go from AttackState to AttackState
        //This rule is set by me, you can change it at will
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