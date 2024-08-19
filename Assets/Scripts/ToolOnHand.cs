using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolOnHand : MonoBehaviour
{
    [SerializeField] Item[] itemsInGame;
    [SerializeField] private GameInput gameInput;

    public void Use(Item item){
        if(item != null){
            if(item.name == "Hoe"){
                TilemapController.instance.TurnIntoPlowableTile(TilemapController.instance.GetGridPosition(gameInput.GetMouseCoordinates()));
            }
            else if(item.name == "CarrotSeed"){
                if(TilemapController.instance.PlantSeed(TilemapController.instance.GetGridPosition(gameInput.GetMouseCoordinates()))){
                    InventoryController.instance.GetSelectedItem(InventoryController.USE_ITEM);
                }
            }
            else if(item.name == "WateringCan"){
                TilemapController.instance.GrowSeed(TilemapController.instance.GetGridPosition(gameInput.GetMouseCoordinates()));
            }
        }
    }
}
