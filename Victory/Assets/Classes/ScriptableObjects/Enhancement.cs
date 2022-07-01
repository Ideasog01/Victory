using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enhancement", menuName = "Enhancement", order = 0)]
public class Enhancement : ScriptableObject
{
    public string enhancementName;

    [TextArea(10, 5)]
    public string enhancementDescription;
}
