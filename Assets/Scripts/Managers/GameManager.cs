using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {get; private set;}
    public bool isGameOver;
    public List<string> possibleInputs = 
    new List<string>()
    {
        "LP",
        "SP",
        "LK",
        "SK",
        "Jump",
        "Crouch"
    };
    
    private void Awake() 
    {
        SetUpGame();
    }

    private void SetUpGame()
    {
        //Cursor.visible = false;
       // Cursor.lockState = CursorLockMode.Locked;

        if(Instance == null) Instance = this;
        else Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
        isGameOver = false;
    }

    public void GameOver(Player winner)
    {
        winner.GetComponent<Animator>().updateMode = AnimatorUpdateMode.UnscaledTime;
        PauseController.TogglePause();
    }

    public void RestartRound()
    {
        PauseController.TogglePause();
        SceneManager.LoadScene(0);
    }    
    
}
