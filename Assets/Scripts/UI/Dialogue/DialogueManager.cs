using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{

    public Text nameText;
    public Text dialogueText;
    public Text dialogueButtonText;

    string oldButtonText;

    bool cursorLocked = false;

    public GameObject dialogueBox;

    public Animator animator;

    private Queue<string> sentences;

    private Queue<string> finishedQuestSentences;

    private Queue<string> afterQuestSentences;

    private Queue<string> finalSentences;

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
        finishedQuestSentences = new Queue<string>();
        afterQuestSentences = new Queue<string>();
        dialogueBox.SetActive(!dialogueBox.activeSelf);
        oldButtonText = dialogueButtonText.text;
    }

    public void StartDialogue(Dialogue dialogue, NpcDialogue npc, Quest quest)
    {

        if (cursorLocked)
        {
            Cursor.lockState = CursorLockMode.Locked;
            cursorLocked = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Confined;
            cursorLocked = true;
        }

        animator.SetBool("isOpen", true);

        nameText.text = dialogue.name;

        sentences.Clear();
        finishedQuestSentences.Clear();
        afterQuestSentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        foreach (string finishedQuestSentence in dialogue.finishedQuestSentences)
        {
            finishedQuestSentences.Enqueue(finishedQuestSentence);
        }

        foreach (string afterQuestSentence in dialogue.afterQuestSentences)
        {
            afterQuestSentences.Enqueue(afterQuestSentence);
        }

        if (!quest)
        {
            if (npc.Helped)
            {
                finalSentences = afterQuestSentences;
            }
            else
            {
                finalSentences = sentences;
            }
        }
        else
        {
            if (!npc.AssignedQuest && !npc.Helped)
            {
                finalSentences = sentences;
            }
            else if (npc.AssignedQuest && !quest.Completed)
            {
                finalSentences = sentences;
            }
            else if (npc.AssignedQuest && quest.Completed)
            {
                finalSentences = finishedQuestSentences;
            }
            else if (npc.Helped)
            {
                finalSentences = afterQuestSentences;
            }
        }
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {

        if (finalSentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        if (finalSentences.Count == 1)
        {
            dialogueButtonText.text = "Close";
        } else
        {
            dialogueButtonText.text = oldButtonText;
        }

        string sentence = finalSentences.Dequeue();
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
        animator.SetBool("isOpen", false);
        if (cursorLocked)
        {
            Cursor.lockState = CursorLockMode.Locked;
            cursorLocked = false;
        }
    }

}
