using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBreath : MonoBehaviour
{
    public AudioSource breath;
    private Collider col;
    private bool breathed = false;


    void Start()
    {
        col = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("Player") && !breathed)
        {
            breath.Play();
            breathed = true;
        }
    }
    
}
