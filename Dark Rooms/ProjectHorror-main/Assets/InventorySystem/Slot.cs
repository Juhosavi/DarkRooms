using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    private Inventory inventory;// a reference to the Inventory script
    public int i;// the index of the slot in the inventory array

    private void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();// get the Inventory script from the "Player" object
    }

    private void Update()
    {
        if(transform.childCount <=0)  // if the slot is empty (has no child objects)
        {
            inventory.isFull[i] = false;// set the corresponding index in the isFull array to false (indicating the slot is not full)
        }
    }

    public void DropItem()
    {
        foreach(Transform child in transform) // loop through each child object in the slot
        {
            child.GetComponent<ItemSpawn>().SpawnDroppedItem();// get the ItemSpawn script attached to the child object and call its SpawnDroppedItem method
            GameObject.Destroy(child.gameObject);// destroy the child object
        }
    }
}
