using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    
    [Header("Enabled only on specific states")]
    public bool onlyOnSpecificStates;
    public GameState[] enabledInStates;

    void Update()
    {
        if (CheckStates())
        {
            GetComponent<Collider>().enabled = true;
        }
        else
        {
            GetComponent<Collider>().enabled = false;
        }
    }

    public void TriggerDialogue ()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }

    void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Player" && CheckStates()) {
            TriggerDialogue();
            Destroy(gameObject);
        }
    }

    bool CheckStates() {
        if(onlyOnSpecificStates) {
            foreach(GameState state in enabledInStates) {
                if(state == GameManager.Instance.state) {
                    return true;
                }
            }
            return false;
        }
        return true;
    }
}
