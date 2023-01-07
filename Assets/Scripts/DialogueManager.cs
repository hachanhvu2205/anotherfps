using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    public GameObject choiceBox;

    public static bool DialogueIsOpen = false;
    private bool choiceActive = false;
    private string currentChoiceContext;
    public Animator animator;

    private Queue<Sentence> sentences;

    Player player;

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<Sentence>();
        player = GameObject.Find("Player").GetComponent<Player>();

        choiceBox.SetActive(false);
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
        if (choiceActive) return;
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        Sentence sentence = sentences.Dequeue();
        StopAllCoroutines();
        nameText.text = sentence.speakerName;
        StartCoroutine(TypeSentence(sentence.sentence));
        if(!ReferenceEquals(sentence.choice, null) && sentence.choice.choices.Length > 0 && sentence.choice.choices.Length <= 3)
        {
            Debug.Log("choiceContext: " + sentence.choice.choiceContext);
            TextMeshProUGUI[] choicesText = choiceBox.GetComponentsInChildren<TextMeshProUGUI>();
            for (int i = 0; i < sentence.choice.choices.Length; i++)
            {
                Debug.Log("choice: " + sentence.choice.choices[i]);
                choicesText[i].text = sentence.choice.choices[i];
                choicesText[i].transform.parent.gameObject.SetActive(true);
            }
            choiceActive = true;
            choiceBox.SetActive(true);
            currentChoiceContext = sentence.choice.choiceContext;
        }
        Debug.Log("sentence: " + sentence.speakerName + ": " + sentence.sentence);
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

    public void Choice1()
    {
        Debug.Log("Choice 1");
        choiceActive = false;
        choiceBox.SetActive(false);
        switch (currentChoiceContext)
        {
            case "FightOrEscape":
                if (GameManager.Instance.state == GameState.Start)
                {
                    Debug.Log("Fight");
                    GameManager.Instance.updateGameState(GameState.Fight);
                }
                break;
            default:
                break;
        }
        DisplayNextSentence();
    }
    
    public void Choice2()
    {
        Debug.Log("Choice 2");
        choiceActive = false;
        choiceBox.SetActive(false);
        switch (currentChoiceContext)
        {
            case "FightOrEscape":
                if (GameManager.Instance.state == GameState.Start)
                {
                    Debug.Log("Escape");
                    GameManager.Instance.updateGameState(GameState.Escape);
                }
                break;
            default:
                break;
        }
        DisplayNextSentence();
    }


}
