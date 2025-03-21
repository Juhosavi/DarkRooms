using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.Burst;
using Unity.VisualScripting;
using UnityEngine;

public class LockedDoors : MonoBehaviour
{

    private TestKeyToDoorSystem testKeyToDoorSystem;

    public Animator door;
    public Collider handleCollider;
    public Collider doorsEnemyTrigger;
    public Collider doorCollider;


    public AudioSource openSound;
    public AudioSource closeSound;
    public AudioSource lockedSound;
    public AudioSource unlockedSound;

    public bool doorisOpen; 
    public bool doorisClosed;

    public GameObject footprintEnemy;


    private void Start() 
    {
        testKeyToDoorSystem = GetComponent<TestKeyToDoorSystem>();

        doorisClosed = true;
        doorisOpen = false;
        
        door.SetBool("Open", false);
        door.SetBool("Closed", true);
    }

    public void DoorHandleMethod()
    {
        if (doorisClosed && testKeyToDoorSystem.canBeOpened == true && doorCollider.enabled && footprintEnemy.activeInHierarchy == false)
        {
            doorCollider.enabled = false;
            handleCollider.enabled = false;
            doorsEnemyTrigger.enabled = false;
            StartCoroutine(preventAnotherOpen());
            door.SetBool("Open", true);
            door.SetBool("Closed", false);
            openSound.Play();
            // AudioManager.instance.PlaySFX("DoorOpen");

            doorisOpen = true;
            doorisClosed = false;
        }
        else if (doorisOpen)
        {
            doorCollider.enabled = false;
            handleCollider.enabled = false;
            StartCoroutine(preventAnotherOpen());
            door.SetBool("Open", false);
            door.SetBool("Closed", true);
            closeSound.Play();
            // AudioManager.instance.PlaySFX("DoorClose");

            doorisClosed = true;
            doorisOpen = false;
        }

    }

    //     private void OnTriggerEnter(Collider other) 
    // {
    //     if (other.gameObject.CompareTag("Enemy"))
    //     {
    //         // pressdoorhandle = GetComponent<PressDoorHandle>();
    //         if (doorisClosed && handleCollider.enabled)
    //         {
    //             handleCollider.enabled = false;
    //             doorsEnemyTrigger.enabled = false;
    //             StartCoroutine(preventAnotherOpen());
    //             door.SetBool("Open", true);
    //             door.SetBool("Closed", false);
    //             openSound.Play();
    //             // AudioManager.instance.PlaySFX("DoorOpen");

    //             doorisOpen = true;
    //             doorisClosed = false;
    //         }
    //     }
    // }

    IEnumerator preventAnotherOpen()
    {
        yield return new WaitForSeconds(1.05f);
        {
            doorCollider.enabled = true;
            handleCollider.enabled = true;
            doorsEnemyTrigger.enabled = true;

            // unlocked = true;
            // lockOB.SetActive(false);
        }
    }
}