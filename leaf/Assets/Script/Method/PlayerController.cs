using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5.0f;
    [SerializeField] private float jumpForce = 5.0f;

    private Rigidbody rb; //컴포넌트를 받아둘 변수
    private bool isGrounded = true;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        #region 하드
        //float h = Input.GetAxisRaw("Horizontal");
        //float v = Input.GetAxisRaw("Vertical");

        //Vector3 dir = new Vector3(h, 0.0f, v); //방향

        ////방향 벡터의 길이가 1보다 크면 (대각선 이동 등) 속도가 너무 빨라지는걸 막기 위해
        //if (dir.magnitude > 1.0f)
        //{
        //    dir = dir.normalized; //방향 벡터를 정규화 한다. (길이를 1로 맞춤)
        //}

        ////실제 이동할 양 계산
        //Vector3 move = dir * moveSpeed * Time.deltaTime;

        //transform.position += move;

        ////스페이스키를 누르면
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    //점프를 해야됨. (대신 땅일 때만 점프)
        //    if (isGrounded)
        //    {
        //        isGrounded = false; //점프하는 순간 땅은 아니기에 false로

        //        //리지드바디의 속도를 가져와서
        //        Vector3 jumpVelocity = rb.velocity;

        //        //Y방향(위쪽) 속도를 jumpForce 값으로 설정하고
        //        jumpVelocity.y = jumpForce;

        //        //수정된 속도를 다시 리지드바디에 넣는다
        //        rb.velocity = jumpVelocity;

        //        //요때 캐릭터가 점프하게 됨
        //    }
        //}

        //if (Input.GetMouseButtonDown(0))
        //{
        //    //공격한다
        //    Debug.Log(123);
        //}
        #endregion
        HandleMove();
        HandleJump();
    }

    //이동관련 메서드
    private void HandleMove()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector3 dir = new Vector3(h, 0.0f, v);

        if (dir.magnitude > 1.0f)
        {
            dir = dir.normalized;
        }

        //실제 이동은 Move메서드가 처리
        Move(dir);
    }

    //실제로 위치를 변경시키는 메서드
    private void Move(Vector3 direction)
    {
        Vector3 move = direction * moveSpeed * Time.deltaTime;
        transform.position += move;
    }

    private void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }
    }

    //점프관련 메서드
    private void Jump()
    {
        isGrounded = false; //점프를 했으니 땅은 아님

        Vector3 jumpVelocity = rb.velocity;
        jumpVelocity.y = jumpForce;
        rb.velocity = jumpVelocity;
    }

    //공격
    private void handleAttack()
    {

    }

    private void Attack()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log(isGrounded);
            isGrounded = true;
        }
    }
}
