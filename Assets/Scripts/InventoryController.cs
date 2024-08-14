using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.ReorderableList;
using UnityEngine;

public class InventoryController : MonoBehaviour{
    private const int MAX_STACKED_ITEMS = 12;
    private const int FIRST_SLOT = 0;
    const bool FULL_INVENTORY = false;
    public InventorySlot[] inventorySlots;
    [SerializeField]private GameObject inventoryItemPrefab;
    private GameInput gameInput;

    int selectedSlot = -1;

    private void Awake(){
        gameInput = FindObjectOfType<GameInput>();
    }

    private void Start(){
        
        gameInput.OnToolSelectedAction += GameInput_OnToolSelectedAction;
        ChangeSelectedSlot(FIRST_SLOT);
    }

    private void GameInput_OnToolSelectedAction(object sender, System.EventArgs e){
        ChangeSelectedSlot(gameInput.GetKeyboardNumberPressed()-1);// 1 is the first number of keyboard numbers
    }

    void ChangeSelectedSlot(int newPosition){
        if(selectedSlot >= 0){ //First iteration out
            inventorySlots[selectedSlot].Deselect();
        }
        inventorySlots[newPosition].Select();
        selectedSlot = newPosition;
    }

    // Search for available slots and call PlaceItemInSlot if found
    public bool AddItem(Item item){
        for(int i = FIRST_SLOT; i < inventorySlots.Length; i++){
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if( itemInSlot != null &&
                itemInSlot.item == item &&
                itemInSlot.count < MAX_STACKED_ITEMS &&
                itemInSlot.item.stackable == true
                ){
                itemInSlot.count++;
                itemInSlot.RefreshCount();
                return !FULL_INVENTORY;
            }
        }

        for(int i = FIRST_SLOT; i < inventorySlots.Length; i++){
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if(itemInSlot == null){
                PlaceItemInSlot(item, slot);
                return !FULL_INVENTORY;
            }
        }

        return FULL_INVENTORY;
    }

    private void PlaceItemInSlot(Item item, InventorySlot slot){
        GameObject newItemGO = Instantiate(inventoryItemPrefab, slot.transform);
        InventoryItem inventoryItem = newItemGO.GetComponent<InventoryItem>();
        inventoryItem.InitialiseItem(item);
    }

    public Item GetSelectedItem(){
        InventorySlot slot = inventorySlots[selectedSlot];
        InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
        if(itemInSlot != null){
            return itemInSlot.item;
        }

        return null;
    }
}
