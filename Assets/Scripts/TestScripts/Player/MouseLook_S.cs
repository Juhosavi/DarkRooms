using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook_S : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public Transform playerBody;
    public float xRotation = 0f;
    [SerializeField]
    private GameObject Flashlight;
    [HideInInspector]
    public bool flashlightOn = false;


    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }


    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);

        //taskulamppu menee päälle ja pois F:stä, jos on päällä niin asetetaan bool trueksi.
        //jos bool on true, niin enemyn spherecollider kaksinkertaistuu StatePatternEnemy-scriptissä, eli mobi havaitsee pelaajan myös itsensä takaa kauempaa.
        //koska sphere kasvaa myös ylöspäin niin voi olla ongelma kaksikerroksisessa hotellissa jos mobi on pelaajan yllä/alla -> menee alert stateen
        if (Input.GetKeyDown(KeyCode.F) && Flashlight.activeInHierarchy == false)
        {
            Flashlight.SetActive(true);
            flashlightOn = true;
        }
        else if (Input.GetKeyDown(KeyCode.F) && Flashlight.activeInHierarchy == true)
        {
            Flashlight.SetActive(false);
            flashlightOn = false;
        }
        
    }
}