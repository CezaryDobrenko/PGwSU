using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStates : MonoBehaviour
{

    [HideInInspector]
    public AlertState alertState;
    [HideInInspector]
    public AttackState attackState;
    [HideInInspector]
    public ChaseState chaseState;
    [HideInInspector]
    public PatrolState patrolState;
    [HideInInspector]
    public IEnemyAI currentState;

    void Awake()
    {
        alertState = new AlertState(this);
        attackState = new AttackState(this);
        chaseState = new ChaseState(this);
        patrolState = new PatrolState(this);
    }

    void Start()
    {
        currentState = patrolState;
    }

    void Update()
    {
        currentState.UpdateActions();
    }

    void OnTriggerEnter(Collision otherObj)
    {
        currentState.OnTriggerEnter(otherObj);
    }
}
