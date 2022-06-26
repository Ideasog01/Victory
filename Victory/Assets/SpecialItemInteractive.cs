using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialItemInteractive : Interactable
{
    [SerializeField]
    private SpecialItem specialItem;

    public void EquipSpecialItem()
    {
        PlayerController.playerSpecialItem = specialItem;
        GameManager.playerInterface.SecondaryIcon.sprite = specialItem.specialItemIcon;
        Destroy(this.gameObject);
    }
}
