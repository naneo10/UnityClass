using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerA : MonoBehaviour
{
    private Rigidbody2D rb;

    private float moveSpeed = 5.0f;

    private float inputX;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    void Start()
    {
        
    }

    void Update()
    {
        inputX = Input.GetAxisRaw("Horizontal");
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        Vector2 moveDir = new Vector2(inputX, rb.velocity.y).normalized;
        rb.velocity = moveDir * moveSpeed;
    }

    private void UseJump()
    {

    }
}
