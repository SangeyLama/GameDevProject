using Assets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour {

    public Dialogue initialDialogue;

    public List<Dialogue> dialogueOptions = new List<Dialogue>();

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(initialDialogue);
    }

    public void TriggerDialogueOption(int index)
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogueOptions[index]);
    }
    
}
