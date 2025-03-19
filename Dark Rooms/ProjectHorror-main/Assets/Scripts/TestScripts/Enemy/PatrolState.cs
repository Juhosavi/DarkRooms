using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PatrolState : IEnemyState
{

    StatePatternEnemy enemy;
    int nextWaypoint;

    public PatrolState(StatePatternEnemy statePatternEnemy)//kun statepatternenemyn new patrolstate(); rivi ajetaan ni tää ajetaan
    {
        enemy = statePatternEnemy; //enemy muuttuja on koko StatePatternEnemy -luokka. Näin päästään käsiksi StatePatternEnemyn muuttujiin ja funktioihin.
    }

    public void UpdateState()
    {
        Patrol();
        Look();
    }


    public void ToChaseState()
    {
        Debug.Log("jahdataan");
        enemy.currentState = enemy.chaseState;
    }

    public void ToPatrolState()
    {

    }

    public void ToTrackingState()
    {

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // if (enemy.mouselook_s.flashlightOn)
            // {
            //     Debug.Log("Pelaaja on alueella ja taskulamppu on päällä");
            //     enemy.lastKnownPlayerPosition = enemy.playerRef.transform.position;//tallennetaan pelaajan positio ja mennään sinne (vain patrol-tilassa)
            //     enemy.navMeshAgent.destination = enemy.lastKnownPlayerPosition;
            // }
            // else if (!enemy.mouselook_s.flashlightOn)
            // {
                Debug.Log("Pelaaja on alueella");
                ToAlertState();
            // }
        }
    }

        public void ToAlertState()
    {
        enemy.currentState = enemy.alertState;
    }

    void Patrol()
    {
        enemy.navMeshAgent.isStopped = false;
        enemy.navMeshAgent.speed = 3.5f;

        if(enemy.navMeshAgent.remainingDistance <= enemy.navMeshAgent.stoppingDistance) //enemy on saavuttanut kohteen mihin oli patrollaamassa
        {
            Vector3 point;
            if (RandomPoint(enemy.centerOfPatrolArea.position, enemy.rangeOfPatrolling, out point)) //Uusi patrolpoint
            {
                Debug.DrawRay(point, Vector3.up, Color.blue, 3.0f); //patrolpoint näkyy sinisellä raycastilla, 3s
                enemy.navMeshAgent.SetDestination(point);
            }
        }
    }

    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {

        Vector3 randomPoint = center + Random.insideUnitSphere * range; //random patrol pointti
        UnityEngine.AI.NavMeshHit hit;
        if (UnityEngine.AI.NavMesh.SamplePosition(randomPoint, out hit, 1.0f, UnityEngine.AI.NavMesh.AllAreas)) //dokumentaatio: https://docs.unity3d.com/ScriptReference/AI.NavMesh.SamplePosition.html
        { 
            //the 1.0f is the max distance from the random point to a point on the navmesh, might want to increase if range is big
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
    }

    void Look()
    {
        Debug.DrawRay(enemy.eye.position, enemy.eye.forward * enemy.sightRange, Color.green);
        //^ näköviipaleen keskikohta visualisoitu raylla

        if (enemy.canSeePlayer)
        {
            Debug.Log("pelaaja nähty");
            enemy.chaseTarget = enemy.playerRef.transform;
            ToChaseState();
        }
    }
}
