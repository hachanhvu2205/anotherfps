using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance;
    public GameState state;
    public static event Action<GameState> OnGameStateChanged;

    [Header("Start State Settings")]
    public Door[] doorsInStartState;
    public GameObject[] enemiesInStartState;
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
            case GameState.Save:
                HandleSaveState();
                break;
            case GameState.Fight:
                HandleFightState();
                break;
            case GameState.Escape:
                HandleEscapeState();
                break;
            case GameState.DialogueLoop:
                break;
            default:
                Debug.Log("Invalid game state");
                return;
        }
        Debug.Log("GameManager: Updated Game State to " + state);
        OnGameStateChanged?.Invoke(newState);
    }

    private void HandleEscapeState(){
        foreach (Door door in doorsInStartState){
            door.gameObject.SendMessage("ToggleDoor", false);
        }
    }
    private void HandleSaveState() {
        foreach (Door door in doorsInStartState){
            door.gameObject.SendMessage("ToggleDoor", false);
        }
    }

    private void HandleFightState(){
        foreach (Door door in doorsInStartState){
            door.gameObject.SendMessage("ToggleDoor", false);
        }
    }
    private void HandleStartState(){
        foreach (Door door in doorsInStartState){
            door.gameObject.SendMessage("ToggleDoor", true);
        }
        // foreach (GameObject enemy in enemiesInStartState){
        //     enemy.GetComponent<Enemy>().ToggleEnemy(true);
        // }
    }

}
public enum GameState {
    Start,
    Fight,
    Save,
    Escape,
    DialogueLoop
}