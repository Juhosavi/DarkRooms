using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cabinet : MonoBehaviour
{
    private Animator anim;
    public AudioSource scrape;
    public bool moved = false;

    private void Start() 
    {
        anim = GetComponent<Animator>();

    }

    private void Update() 
    {
        if (anim.enabled == true && !moved)
        {
            moved = true;
            PlayScrape();

        }
    }

    private void PlayScrape()
    {
        scrape.Play();
    }


}
