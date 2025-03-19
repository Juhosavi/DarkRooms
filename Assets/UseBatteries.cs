using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseBatteries : MonoBehaviour
{
    public Light lightComponent;

    // Boolean variable indicating whether the Spot Angle should be increased
    public bool shouldIncreaseSpotAngle = true;

    // Amount to increase the Spot Angle
    public float angleIncreaseAmount = 5f;

    // Current spot angle
    private float originalSpotAngle;

    private void Start()
    {
        originalSpotAngle = lightComponent.spotAngle;
    }

    void Update()
    {
        bool batteriesExist = false;

        foreach (GameObject slot in GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>().slots)
        {
            if (slot.transform.childCount > 0)
            {
                // Debug.Log("Something was added to the inventory");
                if (slot.transform.GetChild(0).GetComponent<ItemSpawn>().item.gameObject.name == "batteries")
                {
                    batteriesExist = true;
                    break;
                }
            }
        }

        if (batteriesExist && shouldIncreaseSpotAngle)
        {
            lightComponent.spotAngle = originalSpotAngle + angleIncreaseAmount;
        }
        else
        {
            lightComponent.spotAngle = originalSpotAngle;
        }
    }
}
