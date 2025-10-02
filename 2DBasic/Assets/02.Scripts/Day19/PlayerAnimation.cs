using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Lumin;

public class PlayerAnimation : MonoBehaviour
{
    [Header("이동/점프")]
    [SerializeField] private float moveSpeed = 5.0f;
    [SerializeField] private float jumpForce = 7.0f;
    [SerializeField] private float avoidSpeed = 20.0f;

    [Header("바닥체크")]
    public Transform groundCheck; //발밑 위치를 나타내는 트랜스폼
    [SerializeField] private float groundCheckRadius = 0.15f; //감지용 반지름
    [SerializeField] private LayerMask groundLayer; //레이어 설정

    //내부에서 참조할 컴포넌트들
    private Animator anim;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    //외부 이펙트
    //private EffectAll effectAll;

    //입력용
    private float inputX; //입력용
    private bool isGrounded; //바닥임?
    private bool jumpRequested; //점프함?
    private bool isAvoid; //피함?
    private bool isAttack; //공격함?

    //입력용 해시
    private static readonly int moveSpeedhash = Animator.StringToHash("Speed");
    private static readonly int jumpHash = Animator.StringToHash("IsJumping");
    private static readonly int avoidHash = Animator.StringToHash("IsAvoid");
    private static readonly int attackHash = Animator.StringToHash("IsAttack");

    //이펙트용 해시 //이벤트용 .cs파일 생성해서 옮기기
    //private static readonly int avoidEffectHash = Animator.StringToHash("isAvoidEffect");

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        inputX = Input.GetAxisRaw("Horizontal");
        anim.SetFloat(moveSpeedhash, Mathf.Abs(rb.velocity.x));

        //방향에 따라 좌우 반전
        if (inputX != 0) //입력값이 0이 아니라면
        {
            if (inputX < 0) //왼쪽
            {
                spriteRenderer.flipX = true;
            }
            else //오른쪽
            {
                spriteRenderer.flipX = false;
            }
        }

        //점프
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            jumpRequested = true;
        }
        //anim.SetFloat("Speed", Mathf.Abs(rb.velocity.x));

        //회피
        if (Input.GetKeyDown(KeyCode.Tab) && isGrounded)
        {
            isAvoid = true;
        }

        //공격
        if (Input.GetKeyDown(KeyCode.R) && isGrounded)
        {
            isAttack = true;
        }
    }

    private void FixedUpdate()
    {
        //EffectAll effectAll = new EffectAll();

        //바닥감지
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
            
        //이동
        rb.velocity = new Vector2(inputX * moveSpeed, rb.velocity.y);

        //회피
        if (!isAvoid)
        {
            anim.SetBool(avoidHash, false);
            /*
            //rb.velocity = new Vector2(inputX * moveSpeed, rb.velocity.y);
            //anim.SetBool(avoidEffectHash, false); 이벤트용 .cs 생성 필요
            //effectAll.AvoidDisActive(); //이펙트 비활성화
            */
        }
        else if (isAvoid)
        {
            //회피
            anim.SetBool(avoidHash, true);
            
            /*
            //anim.SetBool(avoidEffectHash, true); 이벤트용 .cs 생성 필요
            //effectAll.AvoidActive(); //이펙트 활성화
            */
        }
        isAvoid = false;

        //점프
        if (jumpRequested && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

            //에니메이터에 점프라고 알려줌
            anim.SetBool(jumpHash, true);

        }
        jumpRequested = false;

        //다시 바닥에 닿고 거의 내려온 상태라면 점프 종료
        if (isGrounded && rb.velocity.y <= 0.05f)
        {
            anim.SetBool(jumpHash, false);
        }

        //공격
        if (isAttack && isGrounded)
        {
            anim.SetBool(attackHash, true);
            isAttack = false;
        }
        else if (!isAttack)
        {
            anim.SetBool(attackHash, false);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }

    //public bool IsEffectAvoid ()
    //{
    //    bool check = isAvoid;
    //    return check;
    //}
}
