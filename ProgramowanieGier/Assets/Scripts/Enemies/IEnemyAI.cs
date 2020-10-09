﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyAI
{
    void UpdateActions();

    void OnTriggerEnter(Collision enemy);

    void toPatrolState();

    void ToAttackState();

    void ToAlertState();

    void ToChaseState();
}
