using UnityEngine;
using System.Collections;
using UnityEngine.AI;

//This is enemy script
//If you want to create new kind of enemy always 
//add this script to enemy object it defines
//common elements that every enemy should share

public class Enemy : MonoBehaviour
{
    public Sprite deadBody;
    public int maxHealth;

    float health;
    bool isGameOver = false;

    EnemyStates enemyStates;
    NavMeshAgent navigationMesh;
    SpriteRenderer spriteRenderer;
    BoxCollider boxColider;
    Animator animator;
    PlayerHealth sourceControl;

    private void Start()
    {
        health = maxHealth;
        enemyStates = GetComponent<EnemyStates>();
        navigationMesh = GetComponent<NavMeshAgent>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxColider = GetComponent<BoxCollider>();
        animator = GetComponent<Animator>();
        sourceControl = GameObject.Find("Player").GetComponent<PlayerHealth>();
    }

    private void Update()
    {
        if (health <= 0)
        {
            if (boxColider.gameObject.name == "Boss" && isGameOver==false)
            {
                isGameOver = true;
                sourceControl.YouWon();
            }
            EnemyIsKilled();
        }
    }

    void AddDamage(float damage)
    {
        health -= damage;
    }

    void EnemyIsKilled()
    {
        animator.enabled = false;
        enemyStates.enabled = false;
        navigationMesh.enabled = false;
        spriteRenderer.sprite = deadBody;
        boxColider.center = new Vector3(0, -0.8f, 0);
        boxColider.size = new Vector3(1.05f, 0.43f, 0.2f);
    }

}