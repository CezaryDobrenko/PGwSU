using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int heath;
    public bool canMeleeAttack;
    public bool canShoot;
    public float meleeDamage;
    public float shootDamamge;

    public void pistolHit(int damage)
    {
        heath = heath - damage;
    }
}
