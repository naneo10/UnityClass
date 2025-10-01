using UnityEngine;

public class Jump02 : MonoBehaviour
{
    [SerializeField] private float jumpForce = 5.0f;
    [SerializeField] private float moveSpeed = 5.0f;
    [SerializeField] private float groundRadius = 1.0f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck;


    private Rigidbody2D rb;
    private bool isGrounded;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();   
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)&& isGrounded)
        {
            Jumping();
        }
    }
    private void FixedUpdate()
    {
        Move();
        GroundCheck();
    }
    void Jumping()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);

    }
    void Move()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);
    }
    void GroundCheck()
    {
        Collider2D hit = Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayer);

        if(hit!=null)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }
    private void OnDrawGizmos()
    {
        if (groundCheck == null) return;

        Gizmos.color = isGrounded ? Color.green : Color.red;

        Gizmos.DrawWireSphere(groundCheck.position, groundRadius);

        var hit = Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayer);

        Gizmos.color = Color.red;

        if(hit!=null)
        {
            Gizmos.DrawWireCube(hit.bounds.center, hit.bounds.size);
        }

    }
}
