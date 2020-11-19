using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//this script make sure that enemy is looking always at travel destination

public class Vision : MonoBehaviour
{

    Vector3 destination;
    NavMeshAgent navigationMesh;

    private void Awake()
    {
        navigationMesh = GetComponentInParent<NavMeshAgent>();
    }

    void Update()
    {
        if (navigationMesh.enabled)
            destination = transform.parent.GetComponent<EnemyStates>().navMeshAgent.destination;
        else
            destination = transform.position;
        transform.LookAt(destination);
    }
}