using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IEnemyState
{

    StatePatternEnemy enemy;

    public AttackState(StatePatternEnemy statePatternEnemy)//kun statepatternenemyn new patrolstate(); rivi ajetaan ni tää ajetaan
    {
        enemy = statePatternEnemy; //enemy muuttuja on koko StatePatternEnemy -luokka. Näin päästään käsiksi StatePatternEnemyn muuttujiin ja funktioihin.
    }
    // Start is called before the first frame update
    public void ToPatrolState()
    {

    }

    public void ToAttackState()
    {
        
    }

    public void ToTrackingState()
    {
        enemy.attackTimer = 0f;
        enemy.currentState = enemy.trackingState;
    }

    public void ToAlertState()
    {
        enemy.attackTimer = 0f;
        enemy.currentState = enemy.alertState;
    }

    public void OnTriggerEnter(Collider other)
    {

    }

    public void OnCollisionEnter(Collision other) 
    {
        // if (other.gameObject.CompareTag("Player"))
        // {
        //     // enemy.enemysCollider.enabled = false;
        //     enemy.enemysCollider.enabled = false;
        //     enemy.navMeshAgent.speed = 0f;
        //     Debug.Log("TO ATTACK STATE");
        //     ToAttackState();
        //}
    }


    public void ToChaseState()
    {
        enemy.currentState = enemy.chaseState;
    }

    public void UpdateState()
    {
        Attack();
        Look();
    }

    private void Attack()
    {
        // enemy.playerHealth.TakeDamage(35f);

        enemy.attackTimer += Time.deltaTime;

        // Debug.Log("attacking");

        if (enemy.attackTimer > 1.2f)
        {
            Debug.Log(enemy.attackTimer);
            Debug.Log("attacking");
            enemy.enemysCollider.enabled = true;
            enemy.navMeshAgent.speed = 5f;
            ToChaseState();
            
        }

    }

    private void Look()
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
            // ToTrackingState();

        }

    }

}
