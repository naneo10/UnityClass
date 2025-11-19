using System.Collections;
using UnityEngine;


[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{


    [Header("Movement")]
    [SerializeField] float moveSpeed = 5.0f;
    [SerializeField] float jumpForce = 6.0f;
    [SerializeField] float gravity = 20.0f;
    [SerializeField] float rotationSpeed = 10.0f;


    [Header("GroundCheck")]
    [SerializeField] Transform groundPoint;
    [SerializeField] float groundCheckRadius = 0.2f;
    [SerializeField] LayerMask groundLayer;

    [SerializeField] Animator animator;
    CharacterController characterController;

    [SerializeField] Transform cameraTransform;

    private Vector3 velocity;

    //Blend,Attack, Jump
    const string AnimBlend = "Blend";
    const string AnimAttack = "Attack";
    const string AnimJump = "Jump";

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();

        if(cameraTransform==null&& Camera.main!=null)
        {
            cameraTransform = Camera.main.transform;
        }
    }
 
    void Update()
    {
        //움직임을 입력받고
        Vector2 input =  GetMovementInput();

        //카메라가 바라보는 방향계산하고
        Quaternion cameraYaw = GetCameraYaw();
        //입력값, 카메라 방향을 이용해 실제 월드 이동 방향벡터 계산
        Vector3 moveDir = GetMoveDirection(input, cameraYaw);
        //바닥에 닿았냐 확인
        bool isGrounded = CheckGrounded();
        //중력적용,
        HandleGravity(isGrounded);
        //점프입력처리
        HandleJump(isGrounded);
        //최종이동
        MoveCharacter(moveDir);
        //회전처리
        RotateCharacter(moveDir);
        //공격
        HandleAttack();
        //블렌드값 업데이트
        UpdateBlendValue();
    }
    //1.입력처리 메서드(플레이어가 지금 어느 방향키를 누르고 있는지 확인하는 메서드)
    Vector2 GetMovementInput() 
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        return new Vector2(h, v);
    }

    //2.카메라 바라보는 방향 사용 - Y축만
    Quaternion GetCameraYaw()
    {
        if (cameraTransform == null) return Quaternion.identity;


        return Quaternion.Euler(0.0f, cameraTransform.eulerAngles.y, 0.0f);
    }
    //3.이동방향 계산 메서드
    Vector3 GetMoveDirection(Vector2 input, Quaternion cameraYaw)
    {
        Vector3 dir = new Vector3(input.x, 0.0f, input.y);

        return (cameraYaw * dir).normalized;
    }
    //4. 바닥체크메서드
    bool CheckGrounded()
    {
        return Physics.CheckSphere(groundPoint.position, groundCheckRadius, groundLayer);
    }
    //5.중력처리 메서드
    void HandleGravity(bool grounded)
    {
        if(grounded && velocity.y<0.0f)
        {
            velocity.y = -1.0f;
        }
        else
        {
            velocity.y -= gravity * Time.deltaTime;
        }
    }
    //6.점프 처리 메서드
    void HandleJump(bool grounded)
    {
        if (!grounded) return;

        if(Input.GetKeyDown(KeyCode.Space))
        {
            velocity.y = jumpForce;

            if (animator != null)
            {
                Debug.Log("123123");
                 StartCoroutine(PulseBool(AnimJump));
            }
        }
    }
    //실제이동처리 메서드
    void MoveCharacter(Vector3 moveDir)
    {
        Vector3 move = moveDir * moveSpeed;
        move.y = velocity.y;

        characterController.Move(move * Time.deltaTime);
    }
    //회전을 처리하는 메서드
    void RotateCharacter(Vector3 moveDir)
    {
        if (moveDir == Vector3.zero) return;

        Quaternion targetRot = Quaternion.LookRotation(moveDir, Vector3.up);

        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            targetRot,
            rotationSpeed * Time.deltaTime
            );


    }
    //공격처리하는 메서드
    void HandleAttack()
    {
        if (animator == null) return;
        if(Input.GetButtonDown("Fire1"))
        {
            StartCoroutine(PulseBool(AnimAttack));
        }
    }
    //애니메이션 블렌드 값을 업데이트 하는 메서드
    void UpdateBlendValue()
    {
        if (animator == null) return;

        Vector2 vec = new Vector2(
            characterController.velocity.x,
            characterController.velocity.z
            );
        float blend = vec.magnitude / moveSpeed;

        if (blend < 0.0f) blend = 0.0f;
        if (blend > 1.0f) blend = 1.0f;

        animator.SetFloat(AnimBlend, blend);
    }

    //파라미터갱신 코루틴
    IEnumerator PulseBool(string name)
    {
        animator.SetBool(name, true);
        yield return null;
        animator.SetBool(name, false);
    }
}
