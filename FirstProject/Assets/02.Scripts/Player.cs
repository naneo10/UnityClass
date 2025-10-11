using UnityEngine;

public class Player : MonoBehaviour
{
    #region config
    //내부 컴포넌트
    Rigidbody2D rb;
    Collider2D hit;

    //입력용
    public float inputX;
    public bool inGrounded;

    [Header("이동/점프")]
    public float moveSpeed = 5.0f;
    public float jumpForce = 8.0f;

    [Header("바닥체크")]
    public Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.15f;
    [SerializeField] private LayerMask groundLayer;
    #endregion

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        Jump();
    }

    private void FixedUpdate()
    {
        Move();
        GroundCheck();
    }

    #region Method
    public void Move()
    {
        //GetAxis && GetAxisRaw : https://onecoke.tistory.com/entry/%EC%9C%A0%EB%8B%88%ED%8B%B0-GetAxis%EC%99%80-GetAxisRaw
        inputX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(inputX * moveSpeed, rb.velocity.y);
    }

    public void Jump()
    {
        if(Input.GetKeyDown(KeyCode.Space) && inGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    public void GroundCheck()
    {
        hit = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if(hit != null)
        {
            inGrounded = true;
        }
        else
        {
            inGrounded = false;
        }
    }

    private void OnDrawGizmos()
    {
        if (groundCheck == null) return;

        Gizmos.color = inGrounded ? Color.green : Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
    #endregion
}
