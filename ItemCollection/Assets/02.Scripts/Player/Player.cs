using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region field
    public float moveSpeed = 2.0f;
    public float runSpeed = 4.0f;
    public float jumpForce = 5.0f;
    public float rotationSpeed = 13.0f;

    private Rigidbody rb;
    private Animator anim;
    private bool isGrounded = true;
    private bool canJump = true;

    private static readonly int moveHash = Animator.StringToHash("Move");
    private static readonly int ySpeedHash = Animator.StringToHash("YSpeed");
    //cs0029 : 해시태그는 int를 반환 / bool도 int로 해서 아래 구문에서 조건식 작성
    private static readonly int isGroundedHash = Animator.StringToHash("isGround");
    private static readonly int leftShiftHash = Animator.StringToHash("LeftShift");
    #endregion

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        Move();
        Jump();
    }

    #region method
    private void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector3 dir = new Vector3(h, 0.0f, v);

        if (dir.sqrMagnitude > 1.0f)
        {
            dir = dir.normalized;
        }

        //달리기
        if (Input.GetKey(KeyCode.LeftShift))
        {
            Vector3 move = transform.position + dir;
            transform.position = Vector3.Lerp(transform.position, move, Time.deltaTime * runSpeed);
            anim.SetBool(leftShiftHash, true);
        }
        else
        {
            Vector3 move = transform.position + dir;
            transform.position = Vector3.Lerp(transform.position, move, Time.deltaTime * moveSpeed);
            anim.SetBool(leftShiftHash, false);
        }

        //회전
        if (dir.sqrMagnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                Time.deltaTime * rotationSpeed
                );
        }

        anim.SetFloat(moveHash, dir.magnitude);
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && canJump)
        {
            canJump = false;

            Vector3 jumpVelocity = rb.velocity;
            jumpVelocity.y = jumpForce;
            rb.velocity = jumpVelocity;

            StartCoroutine(CollTime());
        }
        anim.SetBool(isGroundedHash, isGrounded);
        anim.SetFloat(ySpeedHash, rb.velocity.y);

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    IEnumerator CollTime()
    {
        yield return new WaitForSeconds(2.0f);
        canJump = true;
    }
    #endregion
}
