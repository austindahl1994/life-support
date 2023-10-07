using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;
    public List<GameObject> inventory = new List<GameObject>();
    private bool inventoryOpen;
    public GameObject testItemGet; //remove this is for panel

    private void Awake()
    {
        instance = this;
        inventoryOpen = false;
    }

    //should add item to inventory, then go to item just added
    public void addToInventory() {
        Debug.Log("Adding item to inventory");
        showInventory();
    }

    public void showInventory() {
        Debug.Log("Opening inventory");
        testItemGet.SetActive(true);
        inventoryOpen = true;
    }

    public void closeInventory() {
        if (!inventoryOpen) { return; }
        testItemGet.SetActive(false);
        Debug.Log("Closing Inventory");
        inventoryOpen = false;
    }
}
