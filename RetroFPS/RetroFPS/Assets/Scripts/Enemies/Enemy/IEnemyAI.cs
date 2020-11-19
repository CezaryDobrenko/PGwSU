using UnityEngine;
using System.Collections;

//This is interface script
//DO NOT assign it to object in unity
//It would still work as fine as before 
//but it may confuse you in the future

public interface IEnemyAI
{

    //Define possible actions in actual state of enemy
    void UpdateActions();

    //When trigger is activated: execute code in this method
    void OnTriggerEnter(Collider enemy);

    //Go or return to patrolling
    void ToPatrolState();

    //Attack player if its possible
    void ToAttackState();

    //Search for player if shots fired near enemy or got hit
    void ToAlertState();

    //Chase player if distance between them is to high to attack
    void ToChaseState();

}