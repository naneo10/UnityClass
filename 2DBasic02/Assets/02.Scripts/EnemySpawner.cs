using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("설정")]
    [SerializeField] private BigEnemy bigEnemyPrefab;
    [SerializeField] private int enemyCount = 5; //시작할 때 미리 만들어 놓을 갯수
    [SerializeField] private float spawnInterval = 5.0f; //스폰 간격
    [SerializeField] private float spawnOffsetX = 2.0f; //좌우 스폰 위치
    [SerializeField] private float spawnY = 5.0f; //스폰될 y좌표

    private List<BigEnemy> enemies = new List<BigEnemy>();

    void Start()
    {
        SetupEnemies();
        StartCoroutine(AutoSpawnCo());
    }

    //시작할 때 미리 적을 만들어 놓자
    private void SetupEnemies()
    {
        for(int i = 0; i < enemies.Count; i++)
        {
            var enemy = Instantiate(bigEnemyPrefab, transform); //복제하고
            enemy.gameObject.SetActive(false); //비활성화 하고
            enemies.Add(enemy); //리스트에 추가
        }
    }
    
    //자동스폰 코루틴
    private IEnumerator AutoSpawnCo()
    {
        while (true)
        {
            SpawnOffset(-spawnOffsetX);
            SpawnOffset(spawnOffsetX);
            //지정한 초만큼 대기 후 다시 반복
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    //헬퍼메서드
    private void SpawnOffset(float offset)
    {
        Vector2 pos = new Vector2(offset, spawnY); //좌우로 얼마나 옆에서 소환할 지
        SpawnEnemy(pos); //계산된 위치에 실제 적을 소환
    }

    //스폰메서드
    public void SpawnEnemy(Vector2 spawnPosition)
    {
        BigEnemy enemy = GetFromEnemy();
        enemy.transform.position = spawnPosition; //소환할 위치로

        //1.에너미 초기화
        enemy.Init(spawnPosition, this);

        //2.활성화를 시킴
        enemy.gameObject.SetActive(true);
    }

    //가져오는거
    private BigEnemy GetFromEnemy()
    {
        for (int i = 0; i < enemies.Count; i++) //리스트에 담긴 적들은 순회
        {
            var e = enemies[i]; //i번째 적 참조

            //존재하고 현재 비활성화된 상태라면
            if (e != null && e.gameObject.activeInHierarchy)
            {
                return e; //재사용하겠다
            }
        }

        var newEnemy = Instantiate(bigEnemyPrefab, transform);
        newEnemy.gameObject.SetActive(false);
        enemies.Add(newEnemy);
        return newEnemy;
    }

    //리스폰메서드
    public void RequestRespawn(Vector2 position, float delay)
    {
        StartCoroutine(RespawnDelayCo(position, delay));
    }

    //리스폰 딜레이 코루틴
    private IEnumerator RespawnDelayCo(Vector2 position, float delay)
    {
        yield return new WaitForSeconds(delay);
        SpawnEnemy(position);
    }
}
