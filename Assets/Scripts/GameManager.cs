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
    // public GameObject[] enemiesInStartState;

    [Header("FindKey State Settings")]
    public Door[] doorsInFindKeyState;
    // public GameObject[] enemiesInFindKeyState;

    [Header("FindGun State Settings")]
    public Door[] doorsInFindGunState;
    // public GameObject[] enemiesInFindGunState;

    [Header("FindKeyEnter State Settings")]
    public Door[] doorsInFindKeyEnterState;
    // public GameObject[] enemiesInFindKeyEnterState;

    [Header("FindGunEnter State Settings")]
    public Door[] doorsInFindGunEnterState;
    // public GameObject[] enemiesInFindGunEnterState;

    [Header("Fight State Settings")]
    public Door[] doorsInFightState;
    // public GameObject[] enemiesInFightState;

    [Header("Save State Settings")]
    public Door[] doorsInSaveState;
    // public GameObject[] enemiesInSaveState;

    [Header("Escape State Settings")]
    public Door[] doorsInEscapeState;
    // public GameObject[] enemiesInEscapeState;

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
            case GameState.Start2:
                HandleStartState();
                break;
            case GameState.FindKey:
                HandleFindKeyState();
                break;
            case GameState.FindGun:
                HandleFindGunState();
                break;
            case GameState.FindKeyEnter:
                HandleFindKeyEnterState();
                break;
            case GameState.FindGunEnter:
                HandleFindGunEnterState();
                break;
            case GameState.Save:
                HandleSaveState();
                break;
            case GameState.Fight:
                HandleFightState();
                break;
            case GameState.AfterFight:
                HandleAfterFightState();
                break;
            case GameState.Kill:
                HandleKillState();
                break;
            case GameState.Spare:
                HandleSpareState();
                break;
            case GameState.Escape:
                HandleEscapeState();
                break;
            case GameState.Escaped:
                HandleEscapedState();
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

    private void HandleStartState(){
        ResetObjectStates();
        ToggleDoors(doorsInStartState);
        // foreach (GameObject enemy in enemiesInStartState){
        //     enemy.GetComponent<Enemy>().ToggleEnemy(true);
        // }
    }
    private void HandleFindKeyState(){
        ResetObjectStates();
        ToggleDoors(doorsInFindKeyState);
    }
    private void HandleFindGunState(){
        ResetObjectStates();
        ToggleDoors(doorsInFindGunState);
    }

    private void HandleFindKeyEnterState(){
        ResetObjectStates();
        ToggleDoors(doorsInFindKeyEnterState);
    }

    private void HandleFindGunEnterState(){
        ResetObjectStates();
        ToggleDoors(doorsInFindGunEnterState);
        GameObject.Find("DialogueManager").GetComponent<DialogueManager>().OneLineDialogue("(A door opens in the distance)");
    }

    private void HandleFightState(){
        ResetObjectStates();
        ToggleDoors(doorsInFightState);
        GameObject.Find("CliffDoor").GetComponentInChildren<Door>().gameObject.SendMessage("ToggleDoor", true, SendMessageOptions.DontRequireReceiver);
        GameObject.Find("DialogueManager").GetComponent<DialogueManager>().OneLineDialogue("(A door opens in the distance)");
    }
    private void HandleAfterFightState(){
        ResetObjectStates();
        GameObject.Find("CliffDoor").GetComponentInChildren<Door>().gameObject.SendMessage("ToggleDoor", true, SendMessageOptions.DontRequireReceiver);
        //GameObject.Find("DialogueManager").GetComponent<DialogueManager>().OneLineDialogue("(A door opens in the distance)");
    }
    private void HandleKillState() {
        ResetObjectStates();
        GameObject.Find("HUD").transform.Find("KillScreen").gameObject.SetActive(true);
        FindObjectOfType<GameManager>().SendMessage("RestartGame", 4);
    }
    private void HandleSpareState() {
        ResetObjectStates();
        ToggleDoors(doorsInEscapeState);
    }
    private void HandleSaveState() {
        ResetObjectStates();
        ToggleDoors(doorsInSaveState);
    }
    private void HandleEscapeState(){
        ResetObjectStates();
        ToggleDoors(doorsInEscapeState);
    }
    private void HandleEscapedState(){
        ResetObjectStates();
    }


    private void ResetObjectStates() {
        Door[] allDoors = FindObjectsOfType<Door>();
        foreach (Door door in allDoors){
            door.gameObject.SendMessage("ToggleDoor", false);
        }
        FindObjectOfType<Player>().GetComponent<Player>().SetKills(0);
    }

    private void ToggleDoors(Door[] doors) {
        foreach (Door door in doors){
            door.gameObject.SendMessage("ToggleDoor", true);
        }
    }

}
public enum GameState {
    Start,
    Start2,
    FindKey,
    FindGun,
    FindGunEnter,
    FindKeyEnter,
    Fight,
    AfterFight,
    Kill,
    Spare,
    Save,
    Escape,
    Escaped,
    DialogueLoop
}