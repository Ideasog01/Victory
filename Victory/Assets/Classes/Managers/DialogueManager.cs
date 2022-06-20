using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public static Dialogue currentDialogue;

    [SerializeField]
    private GameObject dialogueInterface;

    [SerializeField]
    private GameObject playerHudPanel;

    [SerializeField]
    private TextMeshProUGUI characterNameText;

    [SerializeField]
    private TextMeshProUGUI dialogueContentText;

    private int _dialogueIndex;

    public void StartDialogue(Dialogue dialogue)
    {
        currentDialogue = dialogue;
        dialogueInterface.SetActive(true);
        playerHudPanel.SetActive(false);

        _dialogueIndex = 0;

        characterNameText.text = currentDialogue.characterName;
        dialogueContentText.text = currentDialogue.dialogueContent[_dialogueIndex];
        PlayerController.disablePlayer = true;
    }

    public void NextDialogue()
    {
        if(currentDialogue != null)
        {
            _dialogueIndex++;

            if(_dialogueIndex > currentDialogue.dialogueContent.Length - 1)
            {
                EndDialogue();
                return;
            }

            characterNameText.text = currentDialogue.characterName;
            dialogueContentText.text = currentDialogue.dialogueContent[_dialogueIndex];
        }
    }

    public void EndDialogue()
    {
        dialogueInterface.SetActive(false);
        playerHudPanel.SetActive(true);
        PlayerController.disablePlayer = false;
    }
}
