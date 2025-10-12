using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    #region config
    //내부 컴포넌트
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Collider2D hit;
    private Animator anim;

    //입력용
    public float inputX;
    public bool inGrounded;
    private float viewHeight;

    [Header("이동/점프")]
    public float moveSpeed = 5.0f;
    public float jumpForce = 8.0f;

    [Header("바닥체크")]
    public Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.15f;
    [SerializeField] private LayerMask groundLayer;

    //애니메이션 해시
    private static readonly int moveHash = Animator.StringToHash("Speed");
    private static readonly int jumpHash = Animator.StringToHash("Jumping");

    #endregion

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        viewHeight = Camera.main.orthographicSize * 2;
    }

    void Update()
    {
        Jump();
        Respawn();
        Direction();
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

        anim.SetFloat(moveHash, Mathf.Abs(rb.velocity.x));
    }

    public void Jump()
    {
        if(Input.GetKeyDown(KeyCode.Space) && inGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            anim.SetBool(jumpHash, true);
        }

        if (inGrounded && rb.velocity.y <= 0.05f)
        {
            anim.SetBool(jumpHash, false);
        }
    }

    public void Direction()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            sr.flipX = true;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            sr.flipX = false;
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

    public void Respawn()
    {
        if(groundCheck.position.y < -viewHeight)
        {
            SceneManager.LoadScene("SampleScene");
        }
    }
    #endregion
}
