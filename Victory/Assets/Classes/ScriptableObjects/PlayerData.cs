using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "PlayerData", order = 0)]
public class PlayerData : ScriptableObject
{
    public Item[] inventoryArray;

    public int currentSceneIndex;

    public Vector3 positionOnExit;
}
