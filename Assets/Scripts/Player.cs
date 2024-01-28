using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed;

    private float xAxis;

    private Rigidbody2D rb;
    private Animator an;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        an = GetComponent<Animator>();
    }

    void Update()
    {
        Move();
        Run();
    }
    private void LateUpdate()
    {
        if (rb.velocity.x != 0) an.SetBool("isMoving", true);
        else an.SetBool("isMoving", false);
        an.SetFloat("xAxis", xAxis);
    }
    private void Move()
    {
        xAxis = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(xAxis * speed, rb.velocity.y);
    }

    private void Run()
    {
        
    }
}
