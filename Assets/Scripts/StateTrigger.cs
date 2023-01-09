using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateTrigger : MonoBehaviour
{
    [Header("Trigger a game state")]
    public GameState stateToTrigger;
    
    void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Player") {
            GameManager.Instance.updateGameState(stateToTrigger);
        }
    }
}
