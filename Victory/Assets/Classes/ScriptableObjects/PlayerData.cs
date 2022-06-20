using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "PlayerData", order = 0)]
public class PlayerData : ScriptableObject
{
    public List<Item> inventoryItems = new List<Item>();

    public List<int> inventoryAmounts = new List<int>();

    public int maxItemAmount;

    public int currentSceneIndex;

    public Vector3 positionOnExit;

    public int playerLevel;

    public int playerMaxExperience;

    public int playerExperience;
}
