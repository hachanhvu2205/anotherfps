using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;

    public static bool DialogueIsOpen = false;

    public Animator animator;

    private Queue<Sentence> sentences;

    Player player;

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<Sentence>();
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    public void StartDialogue (Dialogue dialogue)
    {
        Debug.Log("Starting conversation with " + dialogue.name);

        // player.EnableMovement(false);
        DialogueIsOpen = true;
        Time.timeScale = 0;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;  

        animator.SetBool("isOpen", true);

        sentences.Clear();

        foreach (Sentence sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence ()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        Sentence sentence = sentences.Dequeue();
        StopAllCoroutines();
        nameText.text = sentence.speakerName;
        StartCoroutine(TypeSentence(sentence.sentence));
        Debug.Log(sentence);
    }

    IEnumerator TypeSentence (string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    void EndDialogue ()
    {
        Debug.Log("End of conversation.");

        // player.EnableMovement(true);
        DialogueIsOpen = false;
        Time.timeScale = 1;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        animator.SetBool("isOpen", false);
    }
}
