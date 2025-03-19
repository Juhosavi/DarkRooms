using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SwitchForCabinet : MonoBehaviour
{
    public GameObject player;
    public GameObject cabinet;
    public AudioSource flip;
    private float distanceToButton;

    public bool flipped = false;

    private void Start() 
    {
        player = GameObject.FindGameObjectWithTag("Player");
        
    }
    

    private void Update() 
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        distanceToButton = Vector3.Distance(this.gameObject.transform.position, player.transform.position);


        if (distanceToButton < 2.5f && Physics.Raycast(ray, out hit) && hit.collider.gameObject.tag == "Switch" && Input.GetKey(KeyCode.Mouse0))
        {
            hit.collider.gameObject.GetComponentInChildren<Animator>().enabled = true;
            cabinet.GetComponent<Animator>().enabled = true;

            if(!flipped)
            {
                flip.Play();
                flipped = true;
            }



        }
        
    }
}
