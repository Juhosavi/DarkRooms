using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    private Inventory inventory;// Reference to the player's inventory
    public GameObject itemButton;// The UI element that represents the item being picked up
    public string InteractableObject;


    public void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();// Finds the player's inventory
    }


    public void PickUpItem() // Called when the player picks up the item
    {
        for (int i = 0; i < inventory.slots.Length; i++)// Loop through each slot in the player's inventory
        {

            if (inventory.isFull[i] == false) // If the current slot is not already full
            {
                inventory.isFull[i] = true;// Set the slot to be full
                GameObject uiInstance = Instantiate(itemButton, inventory.slots[i].transform, false);// Instantiate the item button UI element inside the slot
                uiInstance.GetComponent<ItemSpawn>().interactGameObject = InteractableObject;
                AudioManager.instance.PlaySFX("PickUpItem");// Play a sound effect to indicate that the item was picked up
                Destroy(gameObject);// Destroy the item in the game world
                break;// Exit the loop since the item has been successfully picked up
            }
        }
    }
}

