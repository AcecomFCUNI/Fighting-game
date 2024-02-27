using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour
{   
    [Header("Player")]
    [SerializeField] private float jumpForce;
    [SerializeField] private int id = 1;
    [SerializeField] private string character;
    [SerializeField] private GameObject audioWin;
    [SerializeField] private bool isLoser;
    [SerializeField] private Player enemy;
    [SerializeField] private float life;
    [SerializeField] private GameObject k;
    [SerializeField] private GameObject o;
    public static float speed = 7.5f;

    private Rigidbody2D rb;
    private Animator an;

    
    private float xAxis;
    public bool isJump;
    public bool grounded;
    public PlayerData _data;
    private Vector2 jumpVelocity;

    public float XAxis 
    {
        get => xAxis;
        private set => xAxis = value;
    }

    public int Id
    {
        get => id;
        private set => id = value;
    }

    public string Character
    {
        get => character;
        private set => character = value;
    }
    public float Life
    {
        get => life;
        private set
        {
            life = value;
            if (life <= 0) 
            {
                isLoser = true;
                GameManager.Instance.isGameOver = true;
                DoLoseAnimation();
                GameManager.Instance.GameOver(enemy);
                StartCoroutine("SoundKO");
            }
        } 
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        an = GetComponent<Animator>();
        grounded = true;
        isJump = false;
        isLoser = false;
    }

    void Update()
    {
        LookAtEnemy();
        ReadInputs();
        Move();
        Run();
    }

    private void FixedUpdate()
    {
        Jump();
        HandleGravity();
    }

    private void HandleGravity()
    {
        if (!grounded)
        {
            if (transform.position.y >= _data.maxJumpHeight)
            {
                var velocity = rb.velocity;
                velocity = new Vector2(velocity.x, -1*velocity.y);
                rb.velocity = velocity;
            }

            // if (Math.Abs(transform.position.y - (maxJumpHeight - 2f)) < 0.05f)
            // {
            //     if(rb.velocity.y < 0) rb.velocity = jumpVelocity;
            //     else
            //     {
            //         var velocity = rb.velocity;
            //         velocity = new Vector2(velocity.x, -1*velocity.y);
            //         jumpVelocity = velocity;
            //         velocity = new Vector2(velocity.x, 1f);
            //         rb.velocity = velocity;    
            //     }
            //     
            // }

        }
    }

    private void LateUpdate()
    {
        if (rb.velocity.x != 0) an.SetBool("isMoving", true);
        else an.SetBool("isMoving", false);
        an.SetFloat("xAxis", xAxis);
    }

    private void ReadInputs()
    {
        XAxis = Input.GetAxisRaw("Horizontal" + Id);
        if (Input.GetButtonDown("Jump" + Id))
        {
            isJump = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isJump = false;
            grounded = true;
        }
    }
    
    private void Move()
    {
        rb.velocity = new Vector2(xAxis * speed, rb.velocity.y);
    }

    private void Run()
    {
        
    }
    
    private void Jump()
    {
        if (grounded && isJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, _data.jumpForce);
            grounded = false;
            isJump = false;
        } 
        
    }

    private void LookAtEnemy()
    {
        Vector3 lookDirection = enemy.transform.position - transform.position;
        lookDirection.y = 0f;

        if(lookDirection.x > 0) transform.right = new Vector3(1, 0, 0);
        else if(lookDirection.x < 0) transform.right = new Vector3(-1, 0, 0);
    }

    public void TakeDamage(float damage)
    {
        Life -= damage;
    }

    

    public void DoWinAnimation()
    {
        an.Play("Win");
        GameObject audio = Instantiate(audioWin,Vector3.zero, Quaternion.identity);
        Destroy(audio, 3);
    }

    public void DoLoseAnimation()
    {
        an.updateMode = AnimatorUpdateMode.UnscaledTime;
        an.Play("Lose");
    }

    public void LoseAnimationFinished()
    {
        if(enemy.isLoser == false) enemy.DoWinAnimation();
    }

    IEnumerator WinAnimationFinished()
    {
        yield return new WaitForSecondsRealtime(1.5f);
        GameManager.Instance.RestartRound();
    }
    IEnumerator SoundKO()
    {
        GameObject soundK = Instantiate(k,transform.position, Quaternion.identity);
        Destroy(soundK, 3f);
        yield return new WaitForSecondsRealtime(0.15f);
        GameObject soundO = Instantiate(o,transform.position, Quaternion.identity);
        Destroy(soundO, 3f);
    }

}
