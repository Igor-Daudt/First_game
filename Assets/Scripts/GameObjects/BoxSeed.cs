using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSeed : MonoBehaviour
{
    [SerializeField] private Item seed;
    
    public void Interact(){
        InventoryController.instance.AddItem(seed);
    }
}
