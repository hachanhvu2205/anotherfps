using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance;
    public GameState state;
    public static event Action<GameState> OnGameStateChanged;
    private void Awake()
    {
        Instance = this;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Start()
    {
        updateGameState(GameState.Start);
    }

    private void RestartGame(float time)
    {
        StartCoroutine(RestartTimer(time));
    }

    private IEnumerator RestartTimer(float time)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(0);
    }

    public void updateGameState(GameState newState){
        state = newState;

        switch (state) {
            case GameState.Start:
                HandleStartState();
                break;
            case GameState.Fight:
                //HandleFightState();
                break;
            case GameState.Escape:
                HandleEscapeState();
                break;
            case GameState.Death:
                HandleDeathState();
                break;
            case GameState.Pause:
                HandlePauseState();
                break;
            default:
                Debug.Log("Invalid game state");
                return;
        }
        Debug.Log("GameManager: Updated Game State to " + state);
        OnGameStateChanged?.Invoke(newState);
    }

    private void HandleEscapeState(){
    }

    private void HandleDeathState(){
    }

    private void HandlePauseState(){
    }

    private void HandleStartState(){
    }

}
public enum GameState {
    Escape,
    Fight,
    Death,
    Pause,
    Start
}