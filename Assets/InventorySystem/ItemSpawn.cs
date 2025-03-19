using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ItemSpawn : MonoBehaviour
{
   // private FlashlightActivator flashlightActivator;

    public GameObject item;// the item to be spawned
    private Transform spawnPoint;// the position where the item should be spawned

    public string interactGameObject;

    private void Start()
    {
        spawnPoint = GameObject.FindGameObjectWithTag("SpawnPoint").transform; // find the itemspawn point in the scene
       // flashlightActivator = GetComponent<FlashlightActivator>();
    }
    public void SpawnDroppedItem()
    {
            GameObject flashlightInHierarchy = GameObject.FindGameObjectWithTag("Flashlight");

        Instantiate(item, spawnPoint.position, spawnPoint.rotation);// Spawn the item at the spawn point's position and rotation

        if(item.gameObject.name == "FlashlightREAL")
        {
            Debug.Log("Valo pudotettiin");
            flashlightInHierarchy.gameObject.GetComponent<Light>().enabled = false;
        }
    }
}
