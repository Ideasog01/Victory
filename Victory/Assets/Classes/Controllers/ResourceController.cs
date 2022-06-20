using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceController : Interactable
{

    [SerializeField]
    private Item resourceItem;

    [SerializeField]
    private int resourceAmount;

    public void PickupItem()
    {
        Debug.Log("Item Picked Up");
        GameManager.playerController.AddItem(resourceItem, resourceAmount);
        this.gameObject.SetActive(false);
    }
}
