using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class greenhealth : MonoBehaviour
{
    public Animator animator;
    PlayerHealth health;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        health = GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
