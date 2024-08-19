using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolOnHand : MonoBehaviour
{
    private const bool NO_TOP_OBJECTS = false;
    [SerializeField] Camera gameCamera;

    public void Use(Item item){
        if(checkUpperGround() == NO_TOP_OBJECTS){
            if(item != null){
                if(item.name == "Hoe"){
                    TilemapController.instance.TurnIntoPlowableTile(TilemapController.instance.GetGridPosition(GameInput.instance.GetMouseCoordinates()));
                }
                else if(item.name == "CarrotSeed"){
                    if(TilemapController.instance.PlantSeed(TilemapController.instance.GetGridPosition(GameInput.instance.GetMouseCoordinates()), item)){
                        InventoryController.instance.GetSelectedItem(InventoryController.USE_ITEM);
                    }
                }
                else if(item.name == "WateringCan"){
                    TilemapController.instance.GrowSeed(TilemapController.instance.GetGridPosition(GameInput.instance.GetMouseCoordinates()));
                }
                else{
                    TilemapController.instance.HarvestCrop(TilemapController.instance.GetGridPosition(GameInput.instance.GetMouseCoordinates()));
                }
            }
            else{
                TilemapController.instance.HarvestCrop(TilemapController.instance.GetGridPosition(GameInput.instance.GetMouseCoordinates()));
            }
        }
        
    }

    private bool checkUpperGround(){
        Vector3 mousePosition = GameInput.instance.GetMouseCoordinates();
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        RaycastHit2D raycastHit = Physics2D.BoxCast(worldPosition, new Vector2(0.1f, 0.1f), 0f, worldPosition, distance: 0.01f);
        if(raycastHit.collider == true){
            if(raycastHit.transform.TryGetComponent(out BoxSeed boxSeed)){
                boxSeed.Interact();
            }
            return true;
        }
        else{
            return false;
        }
    }
}
