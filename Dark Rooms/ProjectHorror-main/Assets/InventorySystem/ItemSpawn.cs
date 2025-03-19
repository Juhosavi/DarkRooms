using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ItemSpawn : MonoBehaviour
{


    public GameObject item;// the item to be spawned
    private Transform spawnPoint;// the position where the item should be spawned

    public string interactGameObject;

    private void Start()
    {
        spawnPoint = GameObject.FindGameObjectWithTag("SpawnPoint").transform; // find the itemspawn point in the scene
    }
    public void SpawnDroppedItem()
    {

        Instantiate(item, spawnPoint.position, spawnPoint.rotation);// Spawn the item at the spawn point's position and rotation
    }
}
