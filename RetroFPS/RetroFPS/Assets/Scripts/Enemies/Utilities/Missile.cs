using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This is missile that range enemy are using to attack

public class Missile : MonoBehaviour
{
    [HideInInspector]
    public float damage;
    [HideInInspector]
    public float speed;
    Transform player;
    int missileLife;
    float timer;

    void Start()
    {
        missileLife = 15;
        if (GameObject.FindGameObjectWithTag("Player"))
            player = GameObject.FindGameObjectWithTag("Player").transform;
        transform.LookAt(player);
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer > missileLife)
            Destroy(this.gameObject);
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            other.SendMessage("EnemyHit", damage, SendMessageOptions.DontRequireReceiver);
        Destroy(this.gameObject);
    }
}