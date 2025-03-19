using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : IEnemyState
{

    StatePatternEnemy enemy;


    public ChaseState(StatePatternEnemy statePatternEnemy)//kun statepatternenemyn new patrolstate(); rivi ajetaan ni tää ajetaan
    {
        enemy = statePatternEnemy; //enemy muuttuja on koko StatePatternEnemy -luokka. Näin päästään käsiksi StatePatternEnemyn muuttujiin ja funktioihin.
    }

    public void OnTriggerEnter(Collider other)
    {

    }


    public void ToAlertState()
    {
        enemy.currentState = enemy.alertState;
    }

    public void ToChaseState()
    {

    }

    public void ToPatrolState()
    {

    }

    public void ToTrackingState()
    {
        enemy.currentState = enemy.trackingState;
    }

    public void UpdateState()
    {
        Chase();
        Look();
    }

    void Chase()
    {
        enemy.navMeshAgent.speed = 4.5f;
        enemy.indicator.material.color = Color.red;
        enemy.navMeshAgent.destination = enemy.chaseTarget.position;
        enemy.navMeshAgent.isStopped = false;
    }

    void Look()
    {
        Vector3 enemyToTarget = enemy.chaseTarget.position - enemy.eye.position;//suunta silmästä pelaajaan

        Debug.DrawRay(enemy.eye.position, enemyToTarget, Color.yellow);

        //näkösäde on raycast
        RaycastHit hit; //info mihin raycast osuu

        if (Physics.Raycast(enemy.eye.position, enemyToTarget, out hit, enemy.sightRange) && hit.collider.CompareTag("Player"))
        {
            //toteutuu jos säde osuu pelaajaan
            //jos säde osuu pelaajaan, vihu tunnistaa kohteen ja lähtee jahtaamaan
            Debug.Log("pelaaja on näkyvissä");
            enemy.chaseTarget = hit.transform;
        }
        else
        {
            Debug.Log("Pelaaja hävisi");
            ToTrackingState();
        }
    }
}
