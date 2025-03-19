using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public Animator armAnimation;

    private Camera cam;
    [SerializeField]
    private float distance = 3f;
    [SerializeField]
    private LayerMask mask;
    private PlayerUI playerUI;
    private InputManager inputManager;
    public PlayerHealth playerHealth;




    void Start()
    {
        cam = GetComponent<PlayerLook>().cam;
        playerUI = GetComponent<PlayerUI>();
        inputManager = GetComponent<InputManager>();

    }

    
    void Update()
    {
        playerUI.UpdateText(string.Empty);
        Ray ray = new Ray(cam.transform.position, cam.transform.forward); // Luodaan säde, joka lähtee kameran sijainnista eteenpäin.
        Debug.DrawRay(ray.origin, ray.direction * distance); //piirretään säde.
        RaycastHit hitInfo; // tallentaa osuman tiedon
        if(Physics.Raycast(ray, out hitInfo, distance, mask))
        {
            if (hitInfo.collider.GetComponent<Interactable>() != null)
            {
                // Jos törmätyllä objektilla on Interactable-komponentti, tallennetaan se interactable-muuttujaan.
                Interactable interactable = hitInfo.collider.GetComponent<Interactable>();
                playerUI.UpdateText(interactable.promptMessage);
                if(inputManager.onFoot.Interact.triggered)
                {
                    interactable.BaseInteract();

                    armAnimation.ResetTrigger("Interact");
                    armAnimation.SetTrigger("Interact");
    
                }

            }
        }  
    }
}
