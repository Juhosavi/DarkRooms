using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footstep : MonoBehaviour
{
    public AudioSource footstepsSound, sprintSound;// Reference to audio sources for footsteps and sprintingsound
    
    // Reference to PlayerMovement, Examine, and PauseAndInventoryMenu scripts
    public PlayerMovement playerMovement;
    public Examine examine;
    public PauseAndInventoryMenu pauseAndInventoryMenu;


    void Start()
    {
        // Get the PlayerMovement, Examine, and PauseAndInventoryMenu scripts
        playerMovement = GetComponent<PlayerMovement>();
        examine = GetComponentInChildren<Examine>();
        pauseAndInventoryMenu = FindObjectOfType<PauseAndInventoryMenu>();
    }

    void Update()
    {
        if (examine.examineMode == false && pauseAndInventoryMenu.gameIsPaused == false)// Check if examine mode is not active and the game is not paused
        {

            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))// Check if the player is moving
            {
                if (Input.GetKey(KeyCode.LeftShift))// Check if the player is sprinting
                {
                    // Disable footsteps audio source and enable sprint audio source
                    footstepsSound.enabled = false;
                    sprintSound.enabled = true;

                    // Randomize pitch for footsteps and sprint audio sources
                    footstepsSound.pitch = Random.Range(1.1f, 1.6f);
                    sprintSound.pitch = Random.Range(1.7f, 2.6f);

                    if (playerMovement.crouching == true) // Check if the player is crouching, and disable audio sources if true
                    {
                        footstepsSound.enabled = false;
                        sprintSound.enabled = false;
                    }
                }
                else// If the player is not sprinting
                {
                    // Enable footsteps audio source and disable sprint audio source
                    footstepsSound.enabled = true;
                    sprintSound.enabled = false;

                    // Randomize pitch for footsteps and sprint audio sources
                    footstepsSound.pitch = Random.Range(1.1f, 1.6f);
                    sprintSound.pitch = Random.Range(1.7f, 2.6f);

                    if (playerMovement.crouching == true)// Check if the player is crouching, and disable audio sources if true
                    {
                        footstepsSound.enabled = false;
                        sprintSound.enabled = false;
                    }
                }
            }
            else// If the player is not moving
            {
                // Disable both audio sources
                footstepsSound.enabled = false;
                sprintSound.enabled = false;
            }

        }
    }
}


