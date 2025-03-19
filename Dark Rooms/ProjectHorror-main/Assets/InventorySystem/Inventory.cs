using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public bool[] isFull; // Array to store whether each slot is full or not
    public GameObject[] slots;// Array to store references to each slot in the inventory
}
