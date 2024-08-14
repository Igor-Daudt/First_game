using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler{
    
    private Image itemIcon;
    private GameInput gameInput;
    public int count = 1;
    public TextMeshProUGUI countText;
    [HideInInspector]public Item item;
    [HideInInspector]public Transform ParentAfterDrag;

    public void InitialiseItem(Item newItem){
        item = newItem;
        itemIcon.sprite = newItem.icon;
        RefreshCount();
    }

    public void RefreshCount(){
        countText.text = count.ToString();
        bool textActive = count > 1;
        countText.gameObject.SetActive(textActive);
    }

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
