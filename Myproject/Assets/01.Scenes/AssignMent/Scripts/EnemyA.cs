using System.Collections;
using UnityEngine;

public class EnemyA : MonoBehaviour
{
    //총알 프리팹(인스펙터에서 할당)
    [SerializeField] private GameObject bulletPrefab;

    //진행반향
    public enum MovePattern { Up, Down }
    [SerializeField] private MovePattern pattern = MovePattern.Up;

    //타겟이 될 트랜스폼
    private Transform target;

    //발사 수, 각도
    private int fanCount = 2;
    private float fanDeg = 6.0f;

    //최소, 최대 스폰 시간 간격
    private float spawnRateMin = 2.0f;
    private float spawnRateMax = 4.0f;

    //현재 스폰 간격
    private float spawnRate;

    //랜덤
    private int typeRandom;

    void Start()
    {
        //처음 스폰될 시간을 랜덤으로 설정
        spawnRate = Random.Range(spawnRateMin, spawnRateMax);
        //해당 씬에 있는 모든 오브젝트를 검색해서 찾아오기
        target = FindObjectOfType<PlayerControllerA>()?.transform; //Null 조건 연산자
        //발사 타입 랜덤
        typeRandom = Random.Range(1, 3);

        StartCoroutine(SpawnBulletCo());
    }

    private void Update()
    {
        transform.RotateAround(target.position, Vector3.up, 12.0f * Time.deltaTime);
    }
    private IEnumerator SpawnBulletCo()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnRate);
            Vector3 dir = (target.position - transform.position).normalized;

            switch (typeRandom)
            {
                case 1:
                    {
                        FireFan(transform.position, dir, 8.0f);
                    }
                    break;
                case 2:
                    {
                        SpawnShot(transform.position, dir, 8.0f);
                    }
                    break;
            }
            spawnRate = Random.Range(spawnRateMin, spawnRateMax);
            typeRandom = Random.Range(1, 3);
        }
    }

    private void SpawnShot(Vector3 pos, Vector3 dir, float speed)
    {
        GameObject go = Instantiate(bulletPrefab, pos, Quaternion.identity);
        if (go.TryGetComponent<BulletA>(out BulletA bulletA))
        {
            bulletA.Shot(dir, speed);
        }
    }

    private void FireFan (Vector3 pos, Vector3 dir, float speed)
    {
        int half = fanCount / 2;
        for (int i = -half; i <= half; i++)
        {
            float angle = i * fanDeg;

            Vector3 direction = Quaternion.Euler(0, angle, 0) * dir;
            SpawnShot(pos, direction, speed);
        }
    }

    public void Kill()
    {
        gameObject.SetActive(false);
    }

    public void OnDisable()
    {
        GameManagerA.EnemyDisabled();
    }

    public void Win()
    {
        GameManagerA gameManagerA = FindObjectOfType<GameManagerA>();
        gameManagerA.WinGame();
    }
}
