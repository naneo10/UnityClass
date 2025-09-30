
using UnityEngine;
using UnityEngine.UIElements;

public class Jump01 : MonoBehaviour
{
    //∑π¿Ã∑Œ

    [SerializeField] private float jumpForce = 5.0f;
    [SerializeField] private float moveSpeed = 5.0f;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private float rayLength = 2.0f;
    [SerializeField] private LayerMask groundLayer;

    private bool isGround;
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

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            isJumpPressed = true;
        }

        GroundCheck();
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

    void Jumping ()
    {
        if(isJumpPressed)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            isJumpPressed = false;
        }
    }

    void GroundCheck()
    {
        RaycastHit2D hit = Physics2D.Raycast(groundCheck.position, Vector2.down, rayLength, groundLayer);
        if (hit.collider != null)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

        Color rayColor = isGrounded ? Color.green : Color.red;

        Debug.DrawRay(groundCheck.position, Vector2.down * rayLength, rayColor);
    }
}
