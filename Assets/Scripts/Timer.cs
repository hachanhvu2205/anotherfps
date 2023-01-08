using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public GameObject timer;
    private TextMeshProUGUI timerText;
    public float timeRemaining;
    private bool enabled = false;
    
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
        if (enabled)
        {
            // Debug.Log(timeRemaining);
            timeRemaining =  timeRemaining - Time.deltaTime;
            if (timeRemaining < 0)
            {
                timeRemaining = 0;
                enabled = false;
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

    public void setTimeRemaining(float time)
    {
        timeRemaining = time;
        enabled = true;
    }

    void TimerOnGameStateChanged(GameState state)
    {
        Debug.Log("Timer Handling game state changed: " + state);
        if (state == GameState.Start)
        {
            enabled = false;
            timer.SetActive(false);
        }
        else if (state == GameState.Fight)
        {
            enabled = false;
            timer.SetActive(false);
        }
        else if (state == GameState.Escape)
        {
            setTimeRemaining(60);
            enabled = true;
            timer.SetActive(true);
        }
        else
        {
            enabled = false;
            timer.SetActive(false);
        }
    }
}
