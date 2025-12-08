using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //이동
    [Header("PlayerMove")]
    [SerializeField] private float moveSpeed = 5.0f;
    [SerializeField] private float dragThreshold = 5.0f;

    //공격관련
    [Header("AttackSetting")]
    [SerializeField] private float fireInterval = 0.25f;
    [SerializeField] private float bulletLifeTime = 3.0f;
    
    [SerializeField] private Transform firePoint; //발사될 위치

    //컴포넌트
    private Rigidbody rb;
    private Animator anim;
    private Coroutine fireCoroutine;
    private WaitForSeconds waitForFire; //캐싱 변수 사용할 때 마다 new하면 가비지 콜렉터가 계속 돈다 : 자주사용하는건 만들어두는게 좋다

    //입력과 관련된 변수
    //SRP적용시키려면 파일을 4-5개 만들어야되지만 여기다 위임
    private Vector3 dragStartPos; //드래그를 시작할 때 마우스 위치
    private bool isDragging; //드래그 하고 있는지 여부확인
    private float currentMoveX;

    //본체가 움직이고 있는지 확인
    public bool IsMoving { get; private set; }

    //애니메이션 파라미터 : 문자열 대신 미리 만들어두는 습관
    private static readonly int hashRun = Animator.StringToHash("RUN");
    private static readonly int hashIdle = Animator.StringToHash("IDLE");
    private static readonly int hashShoot = Animator.StringToHash("SHOOT");

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();

        waitForFire = new WaitForSeconds(fireInterval); //한 번만 셋팅해두고 계속 사용하겠다
    }

    private void OnEnable()
    {
        currentMoveX = 0.0f;
        fireCoroutine = StartCoroutine(FireLoop());
    }

    private void OnDisable()
    {
        if (fireCoroutine != null)
        {
            StopCoroutine(fireCoroutine);
            fireCoroutine = null; //다시 켰을 때를 대비
        }
        rb.velocity = Vector3.zero;
    }

    void Update()
    {
        //입력처리
        HandleInput();
    }

    private void FixedUpdate()
    {
        //물리
        Vector3 velocity = rb.velocity;
        velocity.x = currentMoveX;
        rb.velocity = velocity;
    }

    private void HandleInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            dragStartPos = Input.mousePosition;
            isDragging = true;
        }
        if (Input.GetMouseButton(0) && isDragging)
        {
            Vector3 currentPos = Input.mousePosition;

            //처음 드래그 시작 위치와 지금 위치의 x축 차이를 계산
            float deltaX = currentPos.x - dragStartPos.x;

            //일정거리 이상 움직였을때만 드래그로 이동했다고 인식
            if(Mathf.Abs(deltaX) >= dragThreshold)
            {
                dragStartPos.x = currentPos.x; //한 번에 너무 많이 튀지 않게 하려고 방지

                //deltaX가 양수면 오른쪽으로 드래그
                if (deltaX > 0) //오른쪽으로 이동
                {
                    currentMoveX = moveSpeed; //x축 양수 방향으로 이동속도 설정
                    ChangeMoveAnimation(true);
                }
                else
                {
                    currentMoveX = -moveSpeed;
                    ChangeMoveAnimation(true);
                }
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
            dragStartPos = Vector3.zero;
            currentMoveX = 0.0f;
            ChangeMoveAnimation(false);
        }
    }

    private void ShootBullet()
    {
        anim.SetTrigger(hashShoot);

        Vector3 spawnPos; //총알이 생성될 위치
        Quaternion spawnRot; //총알이 생성될때의 회전

        if (firePoint != null)
        {
            spawnPos = firePoint.position;
            spawnRot = firePoint.rotation;
        }
        else //없어도 되는 구문
        {
            spawnPos = transform.position + Vector3.forward * 1.0f;
            spawnRot = Quaternion.identity;
        }

        //위치랑 회전 값만 넘겨주기
        BulletPool.Instance.SpawnBullet(spawnPos, spawnRot, bulletLifeTime);
    }

    IEnumerator FireLoop()
    {
        while (true)
        {
            ShootBullet();
            yield return waitForFire;
        }
    }

    //애니메이션 전환
    private void ChangeMoveAnimation(bool isRunning)
    {
        IsMoving = isRunning;

        //둘다 각자 true로 작동할 경우 겹치는 애매한 상황이 생길 수 있어서 아래와 같이 구현
        anim.SetBool(hashRun, isRunning);
        anim.SetBool(hashIdle, !isRunning);
    }
}
