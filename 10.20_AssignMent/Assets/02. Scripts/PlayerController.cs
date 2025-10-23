using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region field
    [Header("이동관련")]
    [SerializeField] private float moveSpeed = 5.0f;
    [SerializeField] private float jumpForce = 7.0f;

    [Header("바닥체크")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundRadius = 0.1f;
    [SerializeField] private LayerMask groundLayer;

    private Rigidbody2D rb;
    private SpriteRenderer sp;
    private Animator anim;

    public float moveInput;
    public bool jumpPressed;
    public bool isGrounded;

    //private static readonly int moveHash = Animator.StringToHash("Move");
    #endregion

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sp = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        moveInput = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump"))
        {
            jumpPressed = true;
        }
    }

    private void FixedUpdate()
    {
        Move();
        Jump();
        MoveDirect();
    }

    #region method
    private void Move()
    {
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
        //anim.SetFloat(moveHash, Mathf.Abs(rb.velocity.x));
        if (Mathf.Abs(moveInput) > 0.01f)
        {
            anim.SetBool("Movebool", true);
        }
        else
        {
            anim.SetBool("Movebool", false);
        }
    }

    private void Jump()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayer);

        if (jumpPressed && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
        jumpPressed = false;

        anim.SetBool("IsGround", isGrounded);
        anim.SetFloat("YSpeed", rb.velocity.y);
    }

    private void MoveDirect()
    {
        if (moveInput != null)
        {
            if (moveInput < 0.0f)
            {
                sp.flipX = true;
            }
            else if (moveInput > 0.0f)
            {
                sp.flipX = false;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck == null) return;
        Gizmos.color = isGrounded ? Color.green : Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundRadius);
    }
    #endregion
}
