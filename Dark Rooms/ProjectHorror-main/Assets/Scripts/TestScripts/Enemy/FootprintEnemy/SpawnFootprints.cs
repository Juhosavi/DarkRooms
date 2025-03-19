using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class SpawnFootprints : MonoBehaviour
{
    public GameObject footprintL;
    public GameObject footprintR;
    public GameObject player;
    public NavMeshAgent footprintEnemy;

    public Vector3 footprintSpawnerL, footprintSpawnerR;
    public GameObject footprintSpawnL;
    public GameObject footprintSpawnR;

    public float footPrintInterval = 1.5f;

    Vector3 lastPrintPos;

    public bool lastPrintL;
    public bool lastPrintR;

    public AudioSource cry;
    private bool cryPlaying;
    

    void Start()
    {
        cryPlaying = false;
        lastPrintL = false;
        lastPrintR = true;
        footprintSpawnerL = footprintSpawnL.transform.position;
        footprintEnemy.SetDestination(player.transform.position);
    }

    void Update()
    {
        footprintEnemy.SetDestination(player.transform.position);
        footprintSpawnerL = footprintSpawnL.transform.position;
        footprintSpawnerR = footprintSpawnR.transform.position;
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        float distanceToLastPrint = Vector3.Distance(transform.position, lastPrintPos);

        if (distanceToPlayer < 15f && cryPlaying == false)
        {
            cry.Play();
            cryPlaying = true;
        }

        if (distanceToPlayer <= footprintEnemy.stoppingDistance)
        {
            footprintEnemy.isStopped = true;
        }

        if (distanceToLastPrint > footPrintInterval)
        {
            if (lastPrintR)
            {
                lastPrintPos = Instantiate(footprintL, footprintSpawnerL, footprintSpawnL.transform.rotation).transform.position;
                lastPrintL = true;
                lastPrintR = false;
            }
            else if (lastPrintL)
            {
                lastPrintPos = Instantiate(footprintR, footprintSpawnerR, footprintSpawnR.transform.rotation).transform.position;
                lastPrintR = true;
                lastPrintL = false;
            }
        }
    }
}
