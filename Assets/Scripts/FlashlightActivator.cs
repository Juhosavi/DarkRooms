using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class FlashlightActivator : MonoBehaviour
{
    public GameObject flashlightInHierarchy;

    void Start()
    {
       flashlightInHierarchy = GameObject.FindGameObjectWithTag("Flashlight");
    }

    public void ToggleFlashlight()
    {
        
        foreach (GameObject slot in GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>().slots)
        {
            if (slot.transform.childCount > 0)
            {
                //Debug.Log("Jotain tuli inventaarioon");
                if (slot.transform.GetChild(0).GetComponent<ItemSpawn>().item.gameObject.name == "FlashlightREAL")
                {
                    flashlightInHierarchy.gameObject.GetComponent<Light>().enabled = true;

                    //Debug.Log("Valo p‰‰lle");
                }
                
            }
        }
    }
}

//FlashlightREAL
//FlashlightButtonNew
//FlashlightButtonNew(Clone)