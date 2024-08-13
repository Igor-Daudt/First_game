using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler{
    private Image itemIcon;
    private GameInput gameInput;
    [HideInInspector]public Transform ParentAfterDrag;

    private void Awake(){
        itemIcon = GetComponent<Image>();
        gameInput = FindObjectOfType<GameInput>();
    }

    public void OnBeginDrag(PointerEventData eventData){
        itemIcon.raycastTarget = false;
        ParentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        
    }
    public void OnDrag(PointerEventData eventData){
        transform.position = gameInput.GetMouseCoordinates();
    }
    public void OnEndDrag(PointerEventData eventData){
        itemIcon.raycastTarget = true;
        transform.SetParent(ParentAfterDrag);
    }

}
