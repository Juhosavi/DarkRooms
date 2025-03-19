using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertState : IEnemyState
{

    StatePatternEnemy enemy;
    float searchTimer, searchTimer2;
    

    public AlertState(StatePatternEnemy statePatternEnemy)//kun statepatternenemyn new patrolstate(); rivi ajetaan ni tää ajetaan
    {
        enemy = statePatternEnemy; //enemy muuttuja on koko StatePatternEnemy -luokka. Näin päästään käsiksi StatePatternEnemyn muuttujiin ja funktioihin.

    }

    public void OnTriggerEnter(Collider other)
    {
        
    }

    public void OnCollisionEnter(Collision other) 
    {
        
    }

    public void ToAttackState()
    {
        
    }


    public void ToAlertState()
    {

    }

    public void ToChaseState()
    {
        searchTimer = 0f;
        searchTimer2 = 0f;
        enemy.currentState = enemy.chaseState;
        enemy.angle = 360;
    }

    public void ToPatrolState()
    {
        searchTimer = 0f;
        searchTimer2 = 0f;
        enemy.currentState = enemy.patrolState;
        enemy.angle = enemy.previousAngle;
    }

    public void ToTrackingState()
    {

    }

    void Search()
    {
        enemy.anim.SetBool("Walk", false);
        enemy.anim.SetBool("LookAround", true);
        enemy.anim.SetBool("Idle", false);
        enemy.indicator.material.color = Color.yellow;
        enemy.navMeshAgent.isStopped = true;
        enemy.transform.Rotate(enemy.searchTurnSpeed * new Vector3(0, 1, 0) * Time.deltaTime);
        searchTimer += Time.deltaTime;
        
        if (searchTimer >= enemy.searchDuration)
        {
            enemy.navMeshAgent.isStopped = true;
            enemy.transform.Rotate(enemy.searchTurnSpeed*2 * new Vector3(0, -1, 0)  * Time.deltaTime);//errrgh. 
            searchTimer2 += Time.deltaTime;

            if (searchTimer2 >= enemy.searchDuration+1f)
            {
            //vihollinen väsyy etsimään. Palataan patrollaa
                ToPatrolState();
            }
        }
    }

    public void UpdateState()
    {
        Look();
        Search();
    }

    void Look()
    {
        Debug.DrawRay(enemy.eye.position, enemy.eye.forward * enemy.sightRange, Color.yellow);

        //näkösäde on raycast
        // RaycastHit hit; //info mihin raycast osuu

        // if ((Physics.Raycast(enemy.eye.position, enemy.eye.forward, out hit, enemy.sightRange) && hit.collider.CompareTag("Player")) || enemy.canSeePlayer)
        if (enemy.canSeePlayer)
        {
            //toteutuu jos säde osuu pelaajaan
            //jos säde osuu pelaajaan, vihu tunnistaa kohteen ja lähtee jahtaamaan
            // Debug.Log("pelaaja nähty");
            enemy.chaseTarget = enemy.playerRef.transform;
            ToChaseState();
        }
    }
}
