using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public Camera cam;
    private float xRotation = 0f;

    public float xSensitivity = 30f;
    public float ySensitivity = 30f;


    void Start()
    {

        Cursor.lockState = CursorLockMode.Locked;
    }
   
    public void ProcessLook(Vector2 input)
    {
        float mouseX = input.x;
        float mouseY = input.y;

        // Lasketaan kameran rotation yl�s ja alas katsomista varten


        xRotation -= (mouseY * Time.deltaTime) * ySensitivity;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);

        // Sovitetaan t�m� rotation kameran rotationiin
        cam.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        // k��nt�� pelaaja kastomaanvasemmalle ja oikealle.
        transform.Rotate(Vector3.up * (mouseX * Time.deltaTime) * xSensitivity);
    }

}
