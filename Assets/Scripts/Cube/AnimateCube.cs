using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateCube : Interactable
{
    Animator animator;
    private string startPromt;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        startPromt = promptMessage;
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if(animator.GetCurrentAnimatorStateInfo(0).IsName("Default"))
        {
            promptMessage = startPromt;
        }
        else
        {
            promptMessage = "Animating..";
        }
        
    }
    protected override void Interact()
    {
        animator.Play("Spin");
    }
}
