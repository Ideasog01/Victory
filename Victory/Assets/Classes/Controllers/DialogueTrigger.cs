using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : Interactable
{
    [SerializeField]
    private Dialogue triggerDialogue;

    public void DialogueStart()
    {
        GameManager.dialogueManager.StartDialogue(triggerDialogue);
    }
}
