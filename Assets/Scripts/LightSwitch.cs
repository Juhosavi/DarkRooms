using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour
{
    public GameObject ceilingLight;// The game object representing the light to be turned on/off
    public bool toggle;// A boolean value representing whether the light is on or off
    public Animator switchAnim;// The animator component responsible for animating the light switch


    public void LightOn()// Turn on the light and play an audio clip
    {
        AudioManager.instance.PlaySFX("LightSwitch");// Play a sound effect using the AudioManager singleton

        // Reset the "press" trigger on the animator and then trigger it again to play the animation
        //switchAnim.ResetTrigger("switch");
        //switchAnim.SetTrigger("switch");

        // Activate the light game object
        ceilingLight.SetActive(true);


        if(toggle == false)// If the toggle variable is currently false, set it to true in 0.5 seconds
        {
            Invoke("ToggleTime", 0.5f);
        }
    }
    public void ToggleTime()// Helper method to set the toggle variable to true
    {
        toggle = true;
    }

    public void LightOff() // Turn off the light if it is currently on
    {
       
        if (toggle == true)// If the toggle variable is true, deactivate the light and set the toggle variable to false
        {
            ceilingLight.SetActive(false);
            toggle = false;
        }
    }
    
}
