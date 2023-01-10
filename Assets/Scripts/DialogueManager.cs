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
    private bool isTyping = false;
    private string currentChoiceContext;
    private Dialogue currentDialogue;
    private Dialogue tempDialogue;
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
        Debug.Log("Starting conversation");

        // player.EnableMovement(false);
        DialogueIsOpen = true;
        Time.timeScale = 0;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;  

        animator.SetBool("isOpen", true);

        sentences.Clear();
        StopAllCoroutines();
        currentDialogue = dialogue;
        foreach (Sentence sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence ()
    {
        if(StopTyping()) return;
        if (choiceActive) return;
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        Sentence sentence = sentences.Peek();
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
        isTyping = true;
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
        sentences.Dequeue();
        isTyping = false;
    }

    bool StopTyping()
    {
        if (isTyping) 
        {
            StopAllCoroutines();
            dialogueText.text = sentences.Dequeue().sentence;
            isTyping = false;
            return true;
        }
        return false;
    }

    void EndDialogue ()
    {
        Debug.Log("End of conversation.");

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
            case "IntroDecision":
                if (GameManager.Instance.state == GameState.Start)
                {
                    Debug.Log("No");
                    Sentence additionalSentence = gameObject.AddComponent<Sentence>();
                    additionalSentence.speakerName = "Commander";
                    additionalSentence.sentence = "Focus up!";
                    Sentence additionalSentence1 = gameObject.AddComponent<Sentence>();
                    additionalSentence1.speakerName = "Commander";
                    additionalSentence1.sentence = "Your mission is to take down the enemy commander.";
                    Sentence additionalSentence2 = gameObject.AddComponent<Sentence>();
                    additionalSentence2.speakerName = "Commander";
                    additionalSentence2.sentence = "Do you understand your mission?";
                    Choice tempchoice = gameObject.AddComponent<Choice>();
                    tempchoice.choices = new string[] {"No", "Yes"};
                    tempchoice.choiceContext =  "IntroDecision";
                    additionalSentence2.choice = tempchoice;
                    Sentence additionalSentence3 = gameObject.AddComponent<Sentence>();
                    additionalSentence3.speakerName = "Commander";
                    additionalSentence3.sentence = "Good! Move out!";
                    Sentence[] tempSentences = new Sentence[4];
                    tempSentences[0] = additionalSentence;
                    tempSentences[1] = additionalSentence1;
                    tempSentences[2] = additionalSentence2;
                    tempSentences[3] = additionalSentence3;

                    Dialogue tempDialogue = gameObject.AddComponent<Dialogue>();
                    tempDialogue.sentences = tempSentences;
                    GameManager.Instance.updateGameState(GameState.DialogueLoop);
                    EndDialogue();
                    StartDialogue(tempDialogue);
                } else if (GameManager.Instance.state == GameState.DialogueLoop)
                {
                    Debug.Log("No");
                    EndDialogue();
                    StartDialogue(currentDialogue);
                }
                break;
            case "FightOrSave":
                if (GameManager.Instance.state == GameState.Start)
                {
                }
                    Debug.Log("Fight");
                    GameManager.Instance.updateGameState(GameState.Fight);
                    StopTyping();
                    DisplayNextSentence();
                break;
            case "KillOrSpare":
                if (GameManager.Instance.state == GameState.AfterFight)
                {
                    Debug.Log("Kill");
                    GameManager.Instance.updateGameState(GameState.Kill);
                    StopTyping();
                    DisplayNextSentence();
                }
                break;
            default:
                break;
        }
        
        
    }
    
    public void Choice2()
    {
        Debug.Log("Choice 2");
        choiceActive = false;
        choiceBox.SetActive(false);
        switch (currentChoiceContext)
        {
            case "IntroDecision":
                Debug.Log("Yes");
                GameManager.Instance.updateGameState(GameState.Start);
                StopTyping();
                DisplayNextSentence();
                break;
            case "FightOrSave":
                Debug.Log("Save");
                GameManager.Instance.updateGameState(GameState.Save);
                StopTyping();
                DisplayNextSentence();
                break;
            case "KillOrSpare":
                if (GameManager.Instance.state == GameState.AfterFight)
                {
                    Debug.Log("Spare");
                    GameManager.Instance.updateGameState(GameState.Spare);
                    StopTyping();
                    DisplayNextSentence();
                }
                break;
            default:
                break;
        }
    }

    public void OneLineDialogue(string line)
    {
        Debug.Log("One line dialogue");
        Dialogue localDialogue = gameObject.AddComponent<Dialogue>();
        localDialogue.sentences = new Sentence[1];
        Sentence localSentence = gameObject.AddComponent<Sentence>();
        localSentence.sentence = line;
        localDialogue.sentences[0] = localSentence;
        StartDialogue(localDialogue);
    }
}
