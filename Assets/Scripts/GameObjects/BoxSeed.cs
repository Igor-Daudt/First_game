using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSeed : MonoBehaviour
{
    [SerializeField] private Item seed;
    [SerializeField] Texture2D cursorNormal;
    [SerializeField] Texture2D cursorHovering;
    
    public void Interact(){
        InventoryController.instance.AddItem(seed);
    }

    void OnMouseEnter()
    {
        Cursor.SetCursor(cursorHovering, new Vector2(0,0), CursorMode.Auto);
        Debug.Log("Mouse is over GameObject.");
    }

    void OnMouseExit()
    {
        Cursor.SetCursor(cursorNormal, new Vector2(0,0), CursorMode.Auto);
        Debug.Log("Mouse is no longer on GameObject.");
    }
}
