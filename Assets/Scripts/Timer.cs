using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public GameObject timer;
    private TextMeshProUGUI timerText;
    public float timeRemaining;
    private bool timerEnabled = false;
    
    // Start is called before the first frame update
    void Start()
    {
        GameManager.OnGameStateChanged += TimerOnGameStateChanged;
        timerText = timer.GetComponentInChildren<TextMeshProUGUI>();
        timer.SetActive(false);
    }

    void OnDestroy()
    {
        GameManager.OnGameStateChanged -= TimerOnGameStateChanged;
    }

    // Update is called once per frame
    void Update()
    {
        if (timerEnabled)
        {
            // Debug.Log(timeRemaining);
            timeRemaining =  timeRemaining - Time.deltaTime;
            if (timeRemaining < 0)
            {
                timeRemaining = 0;
                timerEnabled = false;
                FindObjectOfType<Player>().GetComponent<Player>().TakeDamage(1000);
            }
            DisplayTime(timeRemaining);
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        float milliseconds = Mathf.FloorToInt((timeToDisplay * 100) % 100);

        timerText.text = string.Format("{0:00}:{1:00}.{2:00}", minutes, seconds, milliseconds);
    }

    public void SetTimeRemaining(float time)
    {
        timeRemaining = time;
        timerEnabled = true;
    }

    void TimerOnGameStateChanged(GameState state)
    {
        Debug.Log("Timer Handling game state changed: " + state);
        if (state == GameState.Escape)
        {
            SetTimeRemaining(400);
            timerEnabled = true;
            timer.SetActive(true);
        }
        else if (state == GameState.Spare)
        {
            SetTimeRemaining(300);
            timerEnabled = true;
            timer.SetActive(true);
        }
        else if (state == GameState.Escaped)
        {
            timerEnabled = false;
        }
        else
        {
            timerEnabled = false;
            timer.SetActive(false);
        }
    }
}
