using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Item", order = 0)]
public class Item : ScriptableObject
{
    public int maxItemAmount;

    public Sprite itemIcon;

    public string itemDisplayName;
}