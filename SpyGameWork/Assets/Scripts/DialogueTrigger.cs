using Assets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour {

    public Dialogue initialDialogue;
    public List<Dialogue> dialogueOptions = new List<Dialogue>();

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(this);
    }

    public void TriggerDialogueOption(int index)
    {
        initialDialogue = dialogueOptions[index];
    }
    
}
