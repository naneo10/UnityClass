using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5.0f; //플레이어 이동속도

    //===플레이어===
    private Rigidbody2D rb; //물리 이동용
    private Animator playerAnim; //플레이어 애니메이터
    private float moveInputX; //좌우 입력
    private float moveInputY; //상하 입력

    //===총알관련===
    //프리팹
    public Bullet bulletPrefab; //발사할 총알 프리팹
    public Effect effectPrefab; //이펙트 프리팹

    public Transform firePoint; //총알이 나가는 위치
    [SerializeField] public float fireRate = 0.2f; //발사간격
    [SerializeField] public float nextFireTime; //다음 발사 가능 시간

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerAnim = GetComponent<Animator>();
    }

    private void Start()
    {
        //풀 매니저에 총알과 이펙트 풀 등록
        Managers.Pool.CreatePool(bulletPrefab, 10);
        Managers.Pool.CreatePool(effectPrefab, 10);
    }

    void Update()
    {
        //입력처리
        moveInputX = Input.GetAxisRaw("Horizontal");
        moveInputY = Input.GetAxisRaw("Vertical");

        //발사처리
        //스페이스를 누르고 다음 발사 가능시간이 되었다면 발사하자
        if(Input.GetKey(KeyCode.Space) && Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + fireRate; //다음 발사 가능 시간 계산
            Fire();
        }

        //애니메이션
        PlayerAnimation();
    }

    private void FixedUpdate()
    {
        PlayerMove();
    }

    void PlayerMove()
    {
        //현재 위치 + (방향 * 속도 * 시간) = 다음 위치
        Vector2 newPos = rb.position + new Vector2(moveInputX, moveInputY) * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(newPos);
    }

    void PlayerAnimation()
    {
        //만약 좌우 입력이 있으면 Move 파라미터를 -1, 1로 설정
        if(moveInputX != 0)
        {
            playerAnim.SetInteger("Move", (int)moveInputX);
        }
        //아니면 Idle
        else
        {
            playerAnim.SetInteger("Move", 0);
        }
    }

    void Fire()
    {
        //1.풀에서 총알을 가져오고
        Bullet bullet = Managers.Pool.GetFromPool(bulletPrefab);

        //2.총알의 위치랑 회전을 초기화
        bullet.transform.SetPositionAndRotation(firePoint.position, Quaternion.identity);
    }
}
