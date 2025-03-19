using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pickup : MonoBehaviour
{
    private Inventory inventory;// Reference to the player's inventory
    public GameObject itemButton;// The UI element that represents the item being picked up
    public string InteractableObject;

    public Article article;
    public GameObject inventoryFullText;

    public void Start()
    {
        //inventoryFullText = GameObject.FindGameObjectWithTag("InventoryFullText");
        inventoryFullText = GameObject.FindGameObjectWithTag("InventoryFullText");

        if (gameObject.GetComponent<Article>())
        {
            article = GetComponent<Article>();
        }

        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();// Finds the player's inventory
    }


    public void PickUpItem() // Called when the player picks up the item
    {
        bool inventoryFull = true; // Variable to track if inventory is full

        for (int i = 0; i < inventory.slots.Length; i++)
        {
            if (inventory.isFull[i] == false)
            {
                inventoryFull = false; // Set inventoryFull to false if an empty slot is found

                if (gameObject.CompareTag("Article") && GetComponent<Article>() && article.pickUpFirstTime == false)
                {
                    //Debug.Log("Lue ensin");
                }
                else
                {
                    inventory.isFull[i] = true;
                    GameObject uiInstance = Instantiate(itemButton, inventory.slots[i].transform, false);
                    uiInstance.GetComponent<ItemSpawn>().interactGameObject = InteractableObject;
                    AudioManager.instance.PlaySFX("PickUpItem");
                    Destroy(gameObject);
                    break;
                }
            }
        }

        if (inventoryFull)
        {
           //Debug.Log("Inventaario on täynnä!"); // Display a message when the inventory is full
            inventoryFullText.transform.GetChild(0).gameObject.SetActive(true);
            StartCoroutine(InventoryTextTurnOff());
        }

    }
    IEnumerator InventoryTextTurnOff() 
    { 
        yield return new WaitForSeconds(3);
        {
            inventoryFullText.transform.GetChild(0).gameObject.SetActive(false);
        }
    }
}

