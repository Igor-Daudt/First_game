using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolOnHand : MonoBehaviour
{
    [SerializeField] Item[] itemsInGame;
    [SerializeField] private GameInput gameInput;

    public void Use(Item item){
        Debug.Log(item);
        if(item != null){
            Debug.Log("aqui ");
            if(item.name == "Hoe"){
                TilemapController.instance.ChangeSelectedTile(TilemapController.instance.GetGridPosition(gameInput.GetMouseCoordinates()));
                Debug.Log("Changed");
            }
        }
        
    }
}
