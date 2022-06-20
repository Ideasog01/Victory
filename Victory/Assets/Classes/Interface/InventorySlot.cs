using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private Color noItemColor;

    private int _itemIndex;

    private Image itemIcon;

    private Item inventoryItem;

    private Button removeItemButton;

    public Item InventoryItem
    {
        set { inventoryItem = value; }
        get { return inventoryItem; }
    }

    public int InventorySlotIndex
    {
        get { return _itemIndex; }
        set { _itemIndex = value; }
    }

    private void Awake()
    {
        itemIcon = this.transform.GetChild(0).GetComponent<Image>();
        removeItemButton = this.transform.GetChild(1).GetComponent<Button>();
        removeItemButton.onClick.AddListener(RemoveItem);
    }

    public void DisplayItem()
    {
        if(inventoryItem != null)
        {
            itemIcon.sprite = inventoryItem.itemIcon;
            itemIcon.color = Color.white;
        }
        else
        {
            itemIcon.sprite = null;
            itemIcon.color = noItemColor;
        }
        
    }  
    
    private void RemoveItem()
    {
        if(inventoryItem != null)
        {
            GameManager.inventoryInterface.DisplayRemoveItemPanel(_itemIndex);
        }
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        if(inventoryItem != null)
        {
            GameManager.inventoryInterface.DisplayInventoryItemInfo(this);
        }
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        if(inventoryItem != null)
        {
            GameManager.inventoryInterface.CloseInventoryItemInfo();
        }
    }

}
