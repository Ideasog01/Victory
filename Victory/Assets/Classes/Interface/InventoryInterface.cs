using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryInterface : MonoBehaviour
{
    [Header("Inventory Display Settings")]

    [SerializeField]
    private GameObject playerHud;

    [SerializeField]
    private GameObject inventoryInterface;

    [SerializeField]
    private InventorySlot[] inventorySlots;

    [SerializeField]
    private GameObject inventoryInfoPanel;

    [SerializeField]
    private TextMeshProUGUI inventoryInfoText;

    [Header("Remove Item Display")]

    [SerializeField]
    private GameObject removeItemPanel;

    [SerializeField]
    private TextMeshProUGUI removeItemText;

    [SerializeField]
    private Slider removeItemSlider;

    private bool _displayActive;

    private int _inventorySlotIndex;

    private PlayerData _playerData;

    private void Awake()
    {
        _playerData = GameManager.playerController.playerData;
        removeItemSlider.onValueChanged.AddListener(delegate { RemoveItemSliderChange(); });
    }

    public void DisplayRemoveItemPanel(int index)
    {
        _inventorySlotIndex = index;
        removeItemPanel.SetActive(true);
        removeItemSlider.minValue = 0;
        removeItemSlider.maxValue = _playerData.inventoryAmounts[index];
        removeItemSlider.value = 0;
        removeItemText.text = "REMOVE " + removeItemSlider.value.ToString("F0") + " " + _playerData.inventoryItems[index].itemDisplayName;
    }

    public void RemoveItem()
    {
        if(removeItemSlider.value > 0)
        {
            GameManager.playerController.RemoveItem(_playerData.inventoryItems[_inventorySlotIndex], (int)removeItemSlider.value);
            removeItemPanel.SetActive(false);
        }
    }

    public void RemoveItemSliderChange()
    {
        removeItemText.text = "REMOVE " + removeItemSlider.value.ToString("F0") + " " + _playerData.inventoryItems[_inventorySlotIndex].itemDisplayName;
    }

    public void DisplayInventory()
    {
        if(!_displayActive)
        {
            PlayerController.disablePlayer = true;
            inventoryInterface.SetActive(true);
            playerHud.SetActive(false);

            List<Item> itemList = GameManager.playerController.playerData.inventoryItems;

            foreach (InventorySlot slot in inventorySlots)
            {
                slot.InventoryItem = null;
            }

            for (int i = 0; i < itemList.Count; i++)
            {
                inventorySlots[i].InventoryItem = itemList[i];
                inventorySlots[i].InventorySlotIndex = i;
            }

            foreach (InventorySlot slot in inventorySlots)
            {
                slot.DisplayItem();
            }

            _displayActive = true;
        }
        else
        {
            PlayerController.disablePlayer = false;
            inventoryInterface.SetActive(false);
            playerHud.SetActive(true);
            _displayActive = false;
        }
    }

    public void DisplayInventoryItemInfo(InventorySlot slot)
    {
        bool slotFound = false;
        int index = 0;

        for(int i = 0; i < inventorySlots.Length; i++)
        {
            if (inventorySlots[i] == slot)
            {
                index = i;
                slotFound = true;
                break;
            }
        }

        if(index < GameManager.playerController.playerData.inventoryAmounts.Count)
        {
            if (slotFound)
            {
                inventoryInfoPanel.SetActive(true);
                inventoryInfoPanel.transform.position = slot.transform.position + new Vector3(0, -30, 0);
                inventoryInfoText.text = slot.InventoryItem.itemDisplayName + " X" + GameManager.playerController.playerData.inventoryAmounts[index].ToString();
            }
        }
    }

    public void CloseInventoryItemInfo()
    {
        inventoryInfoPanel.SetActive(false);
    }
}
