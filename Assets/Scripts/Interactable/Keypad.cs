using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keypad : Interactable
{
    [SerializeField]
    private GameObject door;
    private bool doorOpen; // Tieto siitä, onko ovi auki vai kiinni

    void Start()
    {
        
    }


    void Update()
    {
        
    }

    
    protected override void Interact()
    {
        // Vaihdetaan oven tila (avoin/kiinni)
        doorOpen = !doorOpen;
        door.GetComponent<Animator>().SetBool("isOpen", doorOpen);
    }
    public void DoorOpen()
    {
        doorOpen = !doorOpen;
        door.GetComponent<Animator>().SetBool("isOpen", doorOpen);
    }
}
