using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class DoorHandleScript : MonoBehaviour
{
    public float distanceToDoorHandle;
    public UnityEvent unityEvent = new UnityEvent();
    public GameObject doorhandle;
    public GameObject player;


    void Start()
    {
        doorhandle = this.gameObject;
    }

    private void OnMouseDown() 
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        distanceToDoorHandle = Vector3.Distance(this.gameObject.transform.position, player.transform.position);

        if (distanceToDoorHandle < 4f && Physics.Raycast(ray, out hit) && hit.collider.gameObject == gameObject)
        {   
            unityEvent.Invoke();
        }
    }
}