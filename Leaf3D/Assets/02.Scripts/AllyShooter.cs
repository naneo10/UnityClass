using System.Collections;
using UnityEngine;

public class AllyShooter : MonoBehaviour
{
    [SerializeField] private PlayerController player;
    [SerializeField] private float fireInterval = 0.25f;
    [SerializeField] private float bulletlifeTime = 3.0f;
    [SerializeField] private Transform firePoint;

    private Animator anim;
    private Coroutine fireCoroutine;
    private WaitForSeconds waitForFire;

    //애니메이션 파라미터 : 문자열 대신 미리 만들어두는 습관
    private static readonly int hashRun = Animator.StringToHash("RUN");
    private static readonly int hashIdle = Animator.StringToHash("IDLE");
    private static readonly int hashShoot = Animator.StringToHash("SHOOT");

    private void Awake()
    {
        anim = GetComponent<Animator>();

        waitForFire = new WaitForSeconds(fireInterval);
    }

    void Update()
    {
        if (player != null && anim != null)
        {
            //플레이어가 움직이고 있는지 플레이어 스크립트에서 가져오자
            bool isRunning = player.IsMoving;
            anim.SetBool(hashRun, isRunning);
            anim.SetBool(hashIdle, !isRunning);
        }
    }

    private void OnEnable()
    {
        fireCoroutine = StartCoroutine(FireLoop());
    }

    private void OnDisable()
    {
        if (fireCoroutine != null)
        {
            StopCoroutine(fireCoroutine);
            fireCoroutine = null;
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
        BulletPool.Instance.SpawnBullet(spawnPos, spawnRot, bulletlifeTime);
    }

    IEnumerator FireLoop()
    {
        while (true)
        {
            ShootBullet();
            yield return waitForFire;
        }
    }

    public void SetPlayer(PlayerController player)
    {
        this.player = player;
    }
}
