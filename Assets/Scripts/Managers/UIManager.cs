using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance {get; private set;}

    [SerializeField] private Image healthBarOne;
    [SerializeField] private Image healthBarTwo;

    private Player playerOne;
    private Player playerTwo;
    private void Awake() 
    {
        SingletonLogic();
    }

    private void Start() 
    {
        playerOne = GameObject.FindWithTag("Player1").GetComponent<Player>();
        playerTwo = GameObject.FindWithTag("Player2").GetComponent<Player>();
    }
    void Update()
    {
        UpdateHealthBar();
    }

    private void SingletonLogic()
    {
        if(Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void UpdateHealthBar()
    {
        healthBarOne.fillAmount = playerOne.Life/100;
        healthBarTwo.fillAmount = playerTwo.Life/100;
    }
}
