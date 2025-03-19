using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TrackingState : IEnemyState
{

    StatePatternEnemy enemy;
    int nextWaypoint;

    public TrackingState(StatePatternEnemy statePatternEnemy)//kun statepatternenemyn new patrolstate(); rivi ajetaan ni tää ajetaan
    {
        enemy = statePatternEnemy; //enemy muuttuja on koko StatePatternEnemy -luokka. Näin päästään käsiksi StatePatternEnemyn muuttujiin ja funktioihin.
    }

    public void UpdateState()
    {
        Track();
        Look();
    }


    public void ToChaseState()
    {
        // Debug.Log("jahdataan");
        enemy.currentState = enemy.chaseState;
    }

    public void ToPatrolState()
    {

    }

    public void ToTrackingState()
    {

    }

    public void ToAttackState()
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
        // }
        
    }


    public void OnTriggerEnter(Collider other)
    {
        // if (other.CompareTag("Player"))
        // {
        //     Debug.Log("Pelaaja on alueella");
        //     ToAlertState();
        // }
    }

        public void ToAlertState()
    {
        enemy.currentState = enemy.alertState;
        enemy.angle = 180f;
    }

    void Track()
    {
        enemy.anim.SetBool("Walk", true);
        enemy.anim.SetBool("Idle", false);
        enemy.anim.SetBool("LookAround", false);
        enemy.indicator.material.color = Color.magenta;
        // enemy.navMeshAgent.destination = enemy.waypoints[nextWaypoint].position;
        enemy.navMeshAgent.destination = enemy.lastKnownPlayerPosition;
        enemy.navMeshAgent.isStopped = false;

        if (enemy.navMeshAgent.remainingDistance <= enemy.navMeshAgent.stoppingDistance && !enemy.navMeshAgent.pathPending)
        {
            //toteutuu vain jos ollaan perillä
            ToAlertState();

            //tee 5. tila, jossa vihollinen jää hetkeksi pyörii satunnaisesti eri suuntiin. palaa patrol-tilaan hetken päästä.
        }
    }

    void Look()
    {
        Debug.DrawRay(enemy.eye.position, enemy.eye.forward * enemy.sightRange, Color.green);

        //näkösäde on raycast
        RaycastHit hit; //info mihin raycast osuu

        if ((Physics.Raycast(enemy.eye.position, enemy.eye.forward, out hit, enemy.sightRange) && hit.collider.CompareTag("Player")) || enemy.canSeePlayer)
        {
            //jos säde osuu target-layerilla olevaan kohteeseen (sillä layerilla on VAIN pelaaja), vihu näkee pelaajan.
            //jos käytössä olisi vanha raycast niin laittaisin "enemy.chaseTarget = hit.transform;
            // Debug.Log("pelaaja nähty");
            enemy.chaseTarget = enemy.playerRef.transform;//jahtaa pelaajaa
            ToChaseState();
        }
    }
}
