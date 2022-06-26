using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Special Item", menuName = "Special Item", order = 0)]
public class SpecialItem : ScriptableObject
{
    public Sprite specialItemIcon;

    public int itemApplyAmount;

    public float itemDuration;

    public float itemCooldown;

    public enum ItemType {HealingWell, FireTotem}

    public ItemType itemType;

    public Transform itemPrefab;

    public Transform itemProjectilePrefab;

    public float itemActiveDistance;
}
