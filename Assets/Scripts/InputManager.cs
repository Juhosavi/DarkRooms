using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerInput playerInput; // Viittaus PlayerInput
    public PlayerInput.OnFootActions onFoot; // Viittaus OnFoot-toimintoihin PlayerInputissa
    private PlayerMovement playerMovement; // Viittaus PlayerMovement
    private PlayerLook look; // Viittaus PlayerLook

    void Awake()
    {
        playerInput = new PlayerInput(); // Luodaan uusi PlayerInput intanssi
        onFoot = playerInput.OnFoot; // Asetetaan onFoot viittaamaan OnFoot-toimintoihin PlayerInputissa

        playerMovement = GetComponent<PlayerMovement>(); // Haetaan PlayerMovement-komponentti
        look = GetComponent<PlayerLook>(); // Haetaan PlayerLook-komponentti

        // Asetetaan toiminnot InputAction-performancen perusteella
        onFoot.Crouch.performed += ctx => playerMovement.Crouch();
    }

    private void FixedUpdate()
    {
        playerMovement.ProcessMove(onFoot.Movement.ReadValue<Vector2>()); // Pelaajan liikkumisen käsittely
    }

    private void LateUpdate()
    {
        look.ProcessLook(onFoot.Look.ReadValue<Vector2>()); // Pelaajan katselun käsittely
    }

    private void OnEnable()
    {
        onFoot.Enable(); // Otetaan käyttöön OnFoot-toiminnot
    }

    private void OnDisable()
    {
        onFoot.Disable(); // Poistetaan käytöstä OnFoot-toiminnot
    }
}
