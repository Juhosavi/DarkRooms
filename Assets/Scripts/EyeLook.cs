using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeLook : MonoBehaviour
{

    public Transform eyeLookAt = null;// The position that the eye should look at
    public float eyeRotateSPeed = 20f;// How quickly the eye should rotate to look at the target
    public Renderer eyeBlinck;// The renderer component that represents the appearance of the eye
    public float blinkEyeTime;// How long it has been since the last time the eye blinked

    void Start()
    {
        eyeLookAt = GameObject.FindGameObjectWithTag("Player").transform;
        eyeBlinck = GetComponent<Renderer>();// Initialize the eyeBlink field by getting the renderer component of this game object
    }


    // Update is called once per frame
    void Update()
    {
        BlinkEye();// Call the BlinkEye method to simulate blinking

        Quaternion rotTarget = Quaternion.LookRotation(eyeLookAt.position - this.transform.position);// Calculate the rotation needed to look at the eyeLookAt position
        this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, rotTarget, eyeRotateSPeed * Time.deltaTime);// Rotate the game object towards the rotTarget quaternion at a speed determined by eyeRotateSpeed
    }

    void BlinkEye()
    {
       
        blinkEyeTime += Time.deltaTime;// Increment blinkEyeTime by the time since the last frame


        if (blinkEyeTime >= 3)// If it has been 3 seconds since the last blink, set the color to black to simulate closing the eye
        {
        eyeBlinck.material.SetColor("_Color", Color.black);
        }
        else// Otherwise, set the color to white to simulate opening the eye
        {
            eyeBlinck.material.SetColor("_Color", Color.white);
        }
        if(blinkEyeTime > 3.05)// If it has been 3.05 seconds since the last blink, reset blinkEyeTime to start the blinking process over again
        {
            blinkEyeTime = 0;
        }
    }
}
