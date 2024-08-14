using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demoscript : MonoBehaviour{
    public InventoryController inventoryManager;
    public Item[] itemsToPickup;

    public void PickupItem(int id){
        inventoryManager.AddItem(itemsToPickup[id]);
    }
}
