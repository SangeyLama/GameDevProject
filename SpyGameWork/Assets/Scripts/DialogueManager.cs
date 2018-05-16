using Assets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour{

    public static DialogueManager instance = null;
    public Text nameText;
    public Text dialogueText;
    public Button[] buttons;
    public Animator animator;
    public bool midDialogue;
    public int noOfChoices;

    private Queue<string> sentences;
        void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        Init();
    }

    void Init()
    {
        sentences = new Queue<string>();
        toggleButtons(false);
    }

    void toggleButtons(bool value)
    {
        foreach (Button b in buttons)
        {
            b.gameObject.SetActive(value);
        }
    }

    public void StartDialogue(Dialogue dialogue)
    {
        midDialogue = true;
        if (dialogue.dialogueChoices.Count > 0)
        {
            noOfChoices = dialogue.dialogueChoices.Count;
            int i = 0;
            foreach(string choice in dialogue.dialogueChoices)
            {
                buttons[i].GetComponentInChildren<Text>().text = choice;
                i++;
            }
        }
            

        animator.SetBool("isOpen", true);
        nameText.text = dialogue.name;
        sentences.Clear();

        foreach(string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
    }

    public void DisplayChoices()
    {
        for (int i = 0; i < noOfChoices; i++)
        {
            buttons[i].gameObject.SetActive(true);
        }
        noOfChoices = 0;
    }

  
    public void DisplayNextSentence()
    {
        if (sentences.Count == 0 && noOfChoices <= 0)
        {
            EndDialogue();
            return;
        }
        else if(sentences.Count == 1 && noOfChoices > 0)
            DisplayChoices();
        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach(char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    void EndDialogue()
    {
        noOfChoices = 0;
        toggleButtons(false);
        midDialogue = false;
        animator.SetBool("isOpen", false);
    }
	
	
}
