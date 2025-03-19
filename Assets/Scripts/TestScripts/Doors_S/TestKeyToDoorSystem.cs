using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestKeyToDoorSystem : Interactable
{
    public bool locked;
    public bool canBeOpened;

    public LockedDoors lockedDoors;



    // Start is called before the first frame update
    void Start()
    {
        lockedDoors= GetComponent<LockedDoors>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    //public void DoorDestroy()
    //{

    //    Destroy(gameObject);

    //}

    protected override void Interact()
    {

        if (locked)
        {
            foreach (GameObject slot in GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>().slots)
            {

                if (slot.transform.childCount > 0)
                {

                    if (slot.transform.GetChild(0).GetComponent<ItemSpawn>().interactGameObject == gameObject.name)
                    {
                        canBeOpened = true;
                        Debug.Log("On avain");
                    }
                    else
                    {
                        Debug.Log("Ei ole oikeaa avainta");
                    }

                }
                else
                {
                    Debug.Log("Ei ole esinettä inventaariossa");
                }
            }

        }

        if (canBeOpened)
        {
            lockedDoors.DoorHandleMethod();
        }
    }
}