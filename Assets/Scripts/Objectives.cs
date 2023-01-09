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
        Player.KillUpdate += PlayerOnKillUpdate;
        objectives = new string[4];
    }

    // Start is called before the first frame update
    void Start()
    {

    }
    
    void OnDestroy()
    {
        GameManager.OnGameStateChanged -= ObjectivesOnGameStateChanged;
        Player.KillUpdate -= PlayerOnKillUpdate;
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
            objectives = new string[4];
            objectives[0] = "Infiltrate the enemy base";
        }
        else if (state == GameState.Start2)
        {
            objectives = new string[4];
            objectives[0] = "Infiltrate the enemy base";
            objectives[1] = "Choose Save: Find the key to open door";
            objectives[2] = "Choose Fight: Defeat 0/3 enemies to open door";
        }
        else if (state == GameState.FindKey)
        {
            objectives = new string[4];
            objectives[0] = "Infiltrate the enemy base";
            objectives[1] = "Use the key to open door";
        }
        else if (state == GameState.FindGun)
        {
            objectives = new string[4];
            objectives[0] = "Infiltrate the enemy base";
            objectives[1] = "Defeat 0/3 enemies to open door";
        }
        else if (state == GameState.FindKeyEnter)
        {
            objectives = new string[4];
            objectives[0] = "Find the Commander";
        }
        else if (state == GameState.FindGunEnter)
        {
            objectives = new string[4];
            objectives[0] = "Find and defeat the Commander";
            objectives[1] = "Defeat 0/7 enemies";
        }
        else if (state == GameState.Fight)
        {
            objectives = new string[4];
            objectives[0] = "Find and defeat the Commander";
            objectives[1] = "Defeat 0/10 enemies";
        }
        else if (state == GameState.Save)
        {
            objectives = new string[4];
            objectives[0] = "Find the Commander";
        }
        else if (state == GameState.Escape)
        {
            objectives = new string[4];
            objectives[0] = "Escape the island with the commander before time runs out";
            objectives[1] = "Find a way to power the plane";
        }
        else
        {
            Debug.Log("Failed attempt to update objectives");
        }
        PrintObjectives();
        SetObjectivesText(objectives);
    }

    void SetObjectivesText(string[] objectives)
    {
        objectivesText.text = "";
        for (int i = 0; i < objectives.Length; i++)
        {
            if (objectives[i] != null)
            {
                Debug.Log("Adding objective: " + objectives[i]);
                objectivesText.text += "- " + objectives[i] + "\n";
            }
        }
    }
    
    void PlayerOnKillUpdate(int kills)
    {
        if (GameManager.Instance.state == GameState.Start)
        {
            // if (kills < 2)
            // {
            //     objectives[0] = "Defeat " + kills + "/2 enemies";
            // }
            // else
            // {
            //     objectives[0] = "Objective Completed";
                
            //     GameManager.Instance.updateGameState(GameState.Fight);
            // }
        }
        else if (GameManager.Instance.state == GameState.FindGun)
        {
            if (kills < 3)
            {
                objectives[1] = "Defeat " + kills + "/3 enemies to open door";
            }
            else
            {
                objectives[1] = "Objective Completed";
                GameManager.Instance.updateGameState(GameState.FindGunEnter);
            }
        }
        else if (GameManager.Instance.state == GameState.FindGunEnter)
        {
            if (kills < 7)
            {
                objectives[1] = "Defeat " + kills + "/7 enemies";
            }
            else
            {
                objectives[1] = "Objective Completed";
                GameManager.Instance.updateGameState(GameState.Fight);
            }
        }
        else if (GameManager.Instance.state == GameState.Fight)
        {
            if (kills < 10)
            {
                objectives[1] = "Defeat " + kills + "/10 enemies";
            }
            else
            {
                objectives[1] = "Objective Completed";
            }
        }
        SetObjectivesText(objectives);
    }

    void PrintObjectives()
    {
        for (int i = 0; i < objectives.Length; i++)
        {
            if (objectives[i] != null)
            {
                Debug.Log("Objective " + i + ": " + objectives[i]);
            }
        }
    }

    
    
}
