using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{   
    [Header("Player")]
    [SerializeField] private float jumpForce;

    private float xAxis;
    private Rigidbody2D rb;
    private Animator an;
    public bool isJump;
    public bool grounded;
    public PlayerData _data;
    private Vector2 jumpVelocity;

    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        an = GetComponent<Animator>();
        grounded = true;
        isJump = false;
    }

    void Update()
    {
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
        xAxis = Input.GetAxisRaw("Horizontal");
        if (Input.GetButtonDown("Jump"))
        {
            isJump = true;
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
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isJump = false;
            grounded = true;
        }
    }
    
}
