using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue", menuName = "Dialogue", order = 0)]
public class Dialogue : ScriptableObject
{
    public string[] dialogueContent;

    public string characterName;
}