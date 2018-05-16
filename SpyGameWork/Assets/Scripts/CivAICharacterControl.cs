using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CivAICharacterControl : MonoBehaviour
{

    public Transform target;
    public float interactRange;
    public DialogueTrigger dialogueTrigger;


    // Use this for initialization
    void Start()
    {
        dialogueTrigger = GetComponent<DialogueTrigger>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("f") && CheckInRange() && !DialogueManager.instance.midDialogue)
        {
            dialogueTrigger.TriggerDialogue();
        }
        else if(Input.GetKeyDown("f") && DialogueManager.instance.midDialogue)
        {
            DialogueManager.instance.DisplayNextSentence();
        }


    }

    private bool CheckInRange()
    {
        Vector3 targetDirection = target.position - transform.position;
        float distance = targetDirection.magnitude;
        if (distance <= interactRange)
            return true;
        else
            return false;
    }
}
