using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Objectives : MonoBehaviour
{
    private string[] objectives;
    private TextMeshProUGUI objectivesText;


    
    void awake()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        objectivesText = GameObject.Find("ObjectivesText").GetComponent<TextMeshProUGUI>();
        GameManager.OnGameStateChanged += HandleGameStateChanged;
    }
    
    void OnDestroy()
    {
        GameManager.OnGameStateChanged -= HandleGameStateChanged;
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }

    void HandleGameStateChanged(GameState state)
    {
        Debug.Log("Handling game state changed: " + state);
        if (state == GameState.Start)
        {
            objectives = new string[1];
            objectives[0] = "Initial Objective";
        }
        else if (state == GameState.Fight)
        {
            objectives = new string[2];
            objectives[0] = "Fight to conquer the Island";
        }
        else if (state == GameState.Escape)
        {
            objectives = new string[2];
            objectives[0] = "Escape The Island";
        }
        else
        {
            objectives = new string[0];
        }
        
        objectivesText.text = "";
        for (int i = 0; i < objectives.Length; i++)
        {
            Debug.Log("Adding objective: " + objectives[i]);
            objectivesText.text += "\u2B24 " + objectives[i] + "\n";
        }
    }
    
    
}
