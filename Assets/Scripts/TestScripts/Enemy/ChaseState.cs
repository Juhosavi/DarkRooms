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

    public void OnCollisionEnter(Collision other) 
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // enemy.enemysCollider.enabled = false;
            enemy.enemysCollider.enabled = false;
            enemy.navMeshAgent.speed = 0f;
            Debug.Log("TO ATTACK STATE");
            enemy.hit.Play();
            ToAttackState();
        }
    }

    public void ToAttackState()
    {
        enemy.attackTimer = 0f;
        enemy.navMeshAgent.speed = 0f;
        enemy.playerHealth.TakeDamage(35f);
        enemy.currentState = enemy.attackState;
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
        enemy.navMeshAgent.speed = 6f;
        enemy.indicator.material.color = Color.red;
        enemy.navMeshAgent.destination = enemy.chaseTarget.position;
        enemy.navMeshAgent.isStopped = false;
        enemy.anim.SetBool("Walk", true);
        enemy.anim.SetBool("Idle", false);
        enemy.anim.SetBool("LookAround", false);
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
            // Debug.Log("pelaaja on näkyvissä");
            enemy.chaseTarget = hit.transform;
            enemy.lastKnownPlayerPosition = enemy.chaseTarget.position;
        }
        else
        {
            // Debug.Log("Pelaaja hävisi");
            ToTrackingState();
        }
    }
}
