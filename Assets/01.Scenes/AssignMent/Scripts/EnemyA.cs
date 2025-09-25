using System.Collections;
using UnityEngine;

public class EnemyA : MonoBehaviour
{
    //총알 프리팹(인스펙터에서 할당)
    [SerializeField] private GameObject bulletPerfab;

    //타겟이 될 트랜스폼
    private Transform target;

    //최소, 최대 스폰 시간 간격
    private float spawnRateMin = 2.0f;
    private float spawnRateMax = 4.0f;

    //현재 스폰 간격
    private float spawnRate;

    void Start()
    {
        //처음 스폰될 시간을 랜덤으로 설정
        spawnRate = Random.Range(spawnRateMin, spawnRateMax);
        //해당 씬에 있는 모든 오브젝트를 검색해서 찾아오기
        target = FindObjectOfType<PlayerControllerA>()?.transform; //Null 조건 연산자

        StartCoroutine(SpawnBulletCo());
    }

    private void Update()
    {
        transform.RotateAround(target.position, Vector3.up, 8f * Time.deltaTime);
    }

    private IEnumerator SpawnBulletCo()
    {
        while (true)
        {
            //현재 설정된 spawnRate만큼 대기
            yield return new WaitForSeconds(spawnRate);

            //1.총알 생성
            GameObject go = Instantiate(bulletPerfab, transform.position, Quaternion.identity);

            //2.목표방향 계산
            Vector3 dir = (target.position - transform.position).normalized;

            //3.타겟을 바라보도록 설정
            go.transform.LookAt(target);

            //4.발사
            if (go.TryGetComponent<BulletA>(out BulletA bullet))
            {
                bullet.Shot(dir, 8.0f);
            }

            //5.다음 스폰 간격을 랜덤으로 설정
            spawnRate = Random.Range(spawnRateMin, spawnRateMax);
        }
    }

    public void Die()
    {
        gameObject.SetActive(false);

        GameManagerA gameManagerA = FindObjectOfType<GameManagerA>();
        gameManagerA.EndGame();
    }
}
