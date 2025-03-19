using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public interface IEnemyState
{
    void UpdateState();
    void OnTriggerEnter(Collider other);
    void ToPatrolState();
    void ToAlertState();
    void ToChaseState();
    void ToTrackingState();
    void ToAttackState();

    void OnCollisionEnter(Collision other);
}
