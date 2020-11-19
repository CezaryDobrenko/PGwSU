using UnityEngine;
using UnityEngine.AI;
using System.Collections;

//This is core AI script for every Enemy
//If you want to create new kind of enemy always 
//add this script to enemy object it defines
//common elements that every enemy should share

public class EnemyStates : MonoBehaviour
{
    public int patrolRange;
    public int attackRange;
    public int shootRange;
    public float stayAlertTime;
    public float viewAngle;
    public float missileDamage;
    public float missileSpeed;
    public bool onlyMelee = false;
    public float meleeDamage;
    public float attackDelay;

    public LayerMask raycastMask;
    public GameObject missile;
    public Transform vision;
    public Transform[] waypoints;

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
    [HideInInspector]
    public NavMeshAgent navMeshAgent;
    [HideInInspector]
    public Transform chaseTarget;
    [HideInInspector]
    public Vector3 lastKnownPosition;

    void Awake()
    {
        alertState = new AlertState(this);
        attackState = new AttackState(this);
        chaseState = new ChaseState(this);
        patrolState = new PatrolState(this);
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        currentState = patrolState;
    }

    void Update()
    {
        currentState.UpdateActions();
    }

    void OnTriggerEnter(Collider otherObj)
    {
        currentState.OnTriggerEnter(otherObj);
    }

    void HiddenShot(Vector3 shotPosition)
    {
        lastKnownPosition = shotPosition;
        currentState = alertState;
    }

    public bool EnemySpotted()
    {
        if (GameObject.FindWithTag("Player"))
        {
            Vector3 direction = GameObject.FindWithTag("Player").transform.position - transform.position;
            float angle = Vector3.Angle(direction, vision.forward);

            if (angle < viewAngle * 0.5f)
            {
                RaycastHit hit;
                if (Physics.Raycast(vision.transform.position, direction.normalized, out hit, patrolRange, raycastMask))
                {
                    if (hit.collider.CompareTag("Player"))
                    {
                        chaseTarget = hit.transform;
                        lastKnownPosition = hit.transform.position;
                        return true;
                    }
                }
            }
        }
        return false;
    }
}