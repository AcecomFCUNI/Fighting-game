using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {get; private set;}
    public static List<string> possibleInputs = 
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
    }
}
