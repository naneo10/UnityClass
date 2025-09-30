
using Unity.Collections;
using UnityEditorInternal;
using UnityEngine;

public class Jump : MonoBehaviour
{
    [SerializeField] private float jumpForce = 5.0f;
    [SerializeField] private float moveSpeed = 5.0f;

    private Rigidbody2D rb;

    private bool isJumpPressed;
    private bool isGrounded;

    private float horizontalInput;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");

        //점프 요청
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            isJumpPressed = true;
        }
    }

    private void FixedUpdate()
    {
        Move();
        Jumping();
    }

    void Move()
    {
        rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);
    }

    void Jumping()
    {
        if (isJumpPressed)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            isJumpPressed = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
