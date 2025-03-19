using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public abstract class Interactable : MonoBehaviour
{
    // Lis‰‰ tai poista InteractionEvent-komponentti t‰st‰ peliobjektista.

    public bool useEvents;
    // Viesti, joka n‰ytet‰‰n pelaajalle 
    [SerializeField]
    public string promptMessage;

    public virtual string OnLook()
    {
        return promptMessage;
    }
    // Metodi, joka palauttaa kohteena ollessa n‰ytett‰v‰n viestin.

    public void BaseInteract()
    {
        if (useEvents)
        {
            GetComponent<InteractionEvent>().OnInteract.Invoke();
            Interact();
        }
    }
    // Perusmetodi vuorovaikutuksen suorittamiseen.
    // Jos k‰ytet‰‰n tapahtumia (useEvents), kutsutaan InteractionEvent-komponentin OnInteract-tapahtumaa.
    // Sen j‰lkeen kutsutaan Interact-metodia.

    protected virtual void Interact()
    {
        // Ei koodia t‰ss‰, t‰m‰ on pohjaluokan funktio, joka tulee ylikirjoittaa aliluokissa.
    }
    // Suojattu virtual-metodi toteuttamista varten.
}
