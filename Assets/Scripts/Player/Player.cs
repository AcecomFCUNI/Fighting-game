using System;
using System.Data.Common;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private int id;
    [SerializeField] private int health = 10000;
    
    public static float speed = 7.5f;
    
    private float _movementInput;
    private float _variableSpeed;
    
    private Rigidbody2D _rigidBody;
    private PlayerInput _playerInput;
    
    private Player _enemy;

    public int Health
    {
        get => health;
        private set
        {
            health = value;
            if (health <= 0) 
            {
                //TODO
            }
        }
    }

    public float MovementInput
    {
        get => _movementInput;
        private set => _movementInput = value;
    }

    public int Id
    {
        get => id;
        private set => id = value;
    }
    
    
    void Start()
    {
        InitializeVariables();
        _playerInput.SwitchCurrentControlScheme("Keyboard" + id, Keyboard.current);
    }

    // Update is called once per frame
    void Update()
    {
        _movementInput = _playerInput.actions["Move"].ReadValue<float>();
        LookAtEnemy();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void InitializeVariables()
    {
        _playerInput = GetComponent<PlayerInput>();
        _rigidBody = GetComponent<Rigidbody2D>();
        _enemy = GameObject.FindWithTag("Player" + (3 - id)).GetComponent<Player>();
        _variableSpeed = speed;
    }
    
    private void Move()
    {
        _rigidBody.linearVelocity = new Vector2(_movementInput * speed, _rigidBody.linearVelocityY);
    }

    private void LookAtEnemy()
    {
        Vector3 lookDirection = _enemy.transform.position - transform.position;
        lookDirection.y = 0f;

        if(lookDirection.x > 0) transform.right = new Vector3(1, 0, 0);
        else if(lookDirection.x < 0) transform.right = new Vector3(-1, 0, 0);
    }
    
    public void TakeDamage(int damage)
    {
        Health -= damage;
        Debug.Log("Health: " + Health);
    }
    
    //TODO fix this
    public void Sprint(InputAction.CallbackContext callbackContext)
    {
        if(callbackContext.performed) _variableSpeed *= 1.5f;
        else if(callbackContext.canceled) _variableSpeed = speed;
    }
}
