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
        //updateGameState(GameState.Escape);
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
            case GameState.Escape:
                HandleEscapeState();
                break;
            case GameState.Death:
                HandleDeathState();
                break;
            case GameState.Pause:
                HandlePauseState();
                break;
            case GameState.Play:
                HandlePlayState();
                break;
            default:
                Debug.Log("Invalid game state");
                break;
        }

        OnGameStateChanged.Invoke(newState);
    }

    private void HandleEscapeState(){
    }

    private void HandleDeathState(){
    }

    private void HandlePauseState(){
    }

    private void HandlePlayState(){
    }

}
public enum GameState {
    Escape,
    Death,
    Pause,
    Play
}