using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    public bool onlyOnSpecificStates;
    public GameState[] enabledInStates;

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
