using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndFadeOut : MonoBehaviour
{
    public CharacterController controller;
    public PlayerLook playerLook;
    public GameObject fadeOut;


    private void Start() 
    {
        playerLook = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerLook>();
        controller = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>();
    }


    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerLook.ySensitivity = 0f;
            playerLook.xSensitivity = 0f;
            fadeOut.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            StartCoroutine(StopPlayerMovement());
        }
    }

    IEnumerator StopPlayerMovement()
    {
        yield return new WaitForSeconds(1.5f);
        {
            controller.enabled = false;
        }
    }

}
