using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Objectives : MonoBehaviour
{
    private string[] objectives;
    private TextMeshProUGUI objectivesText;


    
    void Awake()
    {
        objectivesText = GameObject.Find("ObjectivesText").GetComponent<TextMeshProUGUI>();
        GameManager.OnGameStateChanged += ObjectivesOnGameStateChanged;
    }

    // Start is called before the first frame update
    void Start()
    {

    }
    
    void OnDestroy()
    {
        GameManager.OnGameStateChanged -= ObjectivesOnGameStateChanged;
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }

    void ObjectivesOnGameStateChanged(GameState state)
    {
        Debug.Log("Objectives Handling game state changed: " + state);
        if (state == GameState.Start)
        {
            objectives = new string[1];
            objectives[0] = "Initial Objective";
        }
        else if (state == GameState.Fight)
        {
            objectives = new string[2];
            objectives[0] = "Defeat the Commander";
            objectives[1] = "Conquer the Island";
        }
        else if (state == GameState.Escape)
        {
            objectives = new string[2];
            objectives[0] = "Escape the island before time runs out";
            objectives[1] = "Find a way to power the plane";
        }
        else
        {
            objectives = new string[0];
        }
        
        objectivesText.text = "";
        for (int i = 0; i < objectives.Length; i++)
        {
            if (objectives[i] != null)
            {
                Debug.Log("Adding objective: " + objectives[i]);
                objectivesText.text += "\u2B24 " + objectives[i] + "\n";
            }
        }
    }
    
    
}
