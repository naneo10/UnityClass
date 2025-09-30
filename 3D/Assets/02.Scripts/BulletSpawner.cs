using System.Collections;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    //총알 프리팹(인스펙터에서 할당)
    [SerializeField] private GameObject bulletPrefab;

    //타겟이 될 트랜스폼
    private Transform target;

    //최소, 최대 스폰 시간 간격
    private float spawnRateMin = 0.5f;
    private float spawnRateMax = 3.0f;

    //현재 스폰 간격
    private float spawnRate;

    //추가
    [SerializeField] private int fanCount = 5; //발사수
    [SerializeField] private float fanDeg = 10.0f;

    //추가
    public enum ShotPattern {  normal, Fan }
    [SerializeField] private ShotPattern pattern = ShotPattern.normal;

    void Start()
    {
        //처음 스폰될 시간을 랜덤으로 설정
        spawnRate = Random.Range(spawnRateMin, spawnRateMax);
        //해당 씬에 있는 모든 오브젝트를 검색해서 찾아온다
        target = FindObjectOfType<PlayerController>()?.transform; //Null 조건 연산자

        //if (target == null)
        //{
        //    return;
        //}

        StartCoroutine(SpawnBulletCo());
    }

    private IEnumerator SpawnBulletCo()
    {
        //    while (true)
        //    {
        //        //현재 설정된 spawnRate만큼 대기
        //        yield return new WaitForSeconds(spawnRate);

        //        //Instantiate
        //        //: Unity에서 프리팹을 복제
        //        //: 원본 오브젝트를 사용하여 새로운 인스턴스를 동적으로 만들 때 사용
        //        //: 너무 많은 호출은 성능에 부담
        //        //: 오브젝트 풀링을 사용하여 미리 생성된 오브젝트를 재사용하는 방식이 효과적

        //        //1.총알 생성
        //        //transform.position : 이 스크립트가 붙어있는 오브젝트에 바로 생성하겠다
        //        GameObject go = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

        //        //2.목표방향 계산
        //        Vector3 dir = (target.position - transform.position).normalized;

        //        //3.타겟을 바라보도록 설정
        //        go.transform.LookAt(target);

        //        //4.발사
        //        if(go.TryGetComponent<Bullet>(out Bullet bullet))
        //        {
        //            bullet.Shot(dir, 8.0f);
        //        }

        //        //5. 다음 스폰 간격을 랜덤으로 설정
        //        spawnRate = Random.Range(spawnRateMin, spawnRateMax);
        //    }

        //추가
        while (true)
        {
            yield return new WaitForSeconds(spawnRate);
            Vector3 dir = (target.position - transform.position).normalized;

            switch(pattern)
            {
                case ShotPattern.normal:
                    SpawnShot(transform.position, dir, 8.0f);
                    break;
                case ShotPattern Fan:
                    FireFan(transform.position, dir, 8.0f);
                    break;
            }
            spawnRate = Random.Range(spawnRateMin, spawnRateMax);
        }
    }

    //추가
    private void SpawnShot(Vector3 pos, Vector3 dir, float speed)
    {
        GameObject go = Instantiate(bulletPrefab, pos, Quaternion.identity);
        if(go.TryGetComponent<Bullet>(out Bullet bullet))
        {
            bullet.Shot(dir, speed);
        }
    }

    //추가
    //Sector form
    private void FireFan (Vector3 pos, Vector3 dir, float speed)
    {
        //좌우 대칭 만들기 위해서 2로 나눈다
        int half = fanCount / 2;
        //팬 카운트가 5면 -2, -1, 0, 1, 2
        for (int i = -half; i <= half; i ++)
        {
            //i = -2 angle = -20
            //i = -1 angle = -10
            //i = 0 angle = 0
            //i = 1 angle = 10
            //i = 2 angle = 20
            float angle = i * fanDeg;

            Vector3 direction = Quaternion.Euler(0, angle, 0) * dir;
            SpawnShot(pos, direction, speed);
        }
    }
}
