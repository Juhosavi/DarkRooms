using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    public float speed = 5f;
    public bool isGrounded;
    public float gravity = -9.8f;
    public float jumpHeight = 3f;
    public bool lerpCrouch;
    public float crouchTimer;
    public bool crouching;
    public bool sprinting;
    public float sprintTimer;
    public Animator animator;
    public PlayerHealth playerHealth;
    void Start()
    {
        controller = GetComponent<CharacterController>();

    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = controller.isGrounded; // tarkistaa onko pelaaja maassa

        if(lerpCrouch)
        {
            crouchTimer += Time.deltaTime;
            float p = crouchTimer / 1;
            p *= p;
            if (crouching) // Jos pelaaja kyykk‰‰
            {
             controller.height = Mathf.Lerp(controller.height, 1, p);
                speed = 3f;
            }
            else // Jos pelaaja nousee seisomaan
            {
               controller.height = Mathf.Lerp(controller.height, 2, p);
                speed = 5f;
            }
            if (p > 1)
            {
                lerpCrouch = false;
                crouchTimer = 0;
            }
            
        }
        if (playerHealth.playerDead == true) // Jos pelaaja on kuollut, asetetaan animaatio "isDead" p‰‰lle
        {
            Debug.Log("NYT KUOLTIIN");
            animator.SetBool("isDead", true);
            
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && crouching == false)  // Jos pelaaja painaa vasenta Shift-n‰pp‰int‰ ja ei ole kyykyss‰, nopeutetaan pelaajaa
        {
            speed = 8f;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift) && crouching == false) // Jos pelaaja vapauttaa vasemman Shift-n‰pp‰imen ja ei ole kyykyss‰, palautetaan nopeus normaaliksi
        { 
            speed = 5f;
        }

    }
    public void ProcessMove(Vector2 input)
    {
        // Lasketaan liikkumissuunta k‰ytt‰en input-vektoria
        Vector3 moveDirection = Vector3.zero;
        moveDirection.x = input.x;
        moveDirection.z = input.y;
        
        controller.Move(transform.TransformDirection(moveDirection) * speed * Time.deltaTime); // Liikutetaan pelaajaa k‰ytt‰en CharacterControlleria
        playerVelocity.y += gravity * Time.deltaTime; // P‰ivitet‰‰n pelaajan pystysuuntainen nopeus k‰ytt‰en painovoimaa

        if (isGrounded && playerVelocity.y < 0)
            playerVelocity.y = -2f;
        controller.Move(playerVelocity * Time.deltaTime);
        
    }
      public void Crouch()
    {
        crouching = !crouching;
        crouchTimer = 0;
        lerpCrouch = true;
    }



}

