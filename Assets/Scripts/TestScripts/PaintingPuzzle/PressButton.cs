using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class PressButton : MonoBehaviour
{
    public float distanceToButton;
    public UnityEvent unityEvent = new UnityEvent();
    public GameObject button;
    public GameObject player;
    public Animator anim;
    public AudioSource buttonSound;
    

    void Start()
    {
        button = this.gameObject;
    }

    private void OnMouseDown() 
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        distanceToButton = Vector3.Distance(this.gameObject.transform.position, player.transform.position);
        Debug.Log(distanceToButton);

        if (distanceToButton < 3f && Physics.Raycast(ray, out hit) && hit.collider.gameObject == gameObject)
        {   
            anim = GetComponent<Animator>();
            anim.SetTrigger("pressed");
            unityEvent.Invoke();
            buttonSound.Play();

            
        }
    }
}
