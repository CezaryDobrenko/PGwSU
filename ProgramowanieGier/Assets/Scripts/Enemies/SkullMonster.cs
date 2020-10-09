using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullMonster : Enemy
{

    void pistolHit(int damage)
    {
        heath = heath - damage;
        Debug.Log("Boli mnie! Zdrowie: " + heath);
    }

}
