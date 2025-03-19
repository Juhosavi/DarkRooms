using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SlamDoor : MonoBehaviour
{
    public Collider slamCollider;
    public AudioSource slam;

    private void Start() 
    {
        slamCollider = GetComponent<BoxCollider>();
        
    }
    
    private void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("Player"))
        {
            slamCollider.enabled = false;
            GetComponent<Animator>().enabled = true;
            slam.Play();

        }
        
    }
}
