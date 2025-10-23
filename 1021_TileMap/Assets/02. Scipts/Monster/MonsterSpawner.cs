using System.Collections;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    [Header("monster setting")]
    [SerializeField] MonsterData[] monsterData;
    [SerializeField] WayPointPath path;

    [Header("spawn setting")]
    [SerializeField] float spawnInterval = 2.0f;

    void Start()
    {
        StartCoroutine(SpawnMonster());
    }

    IEnumerator SpawnMonster()
    {
        yield return new WaitForSeconds(1.0f);
        while (true)
        {
            //생성
            Spawn();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void Spawn()
    {
        if (UnityEngine.Random.Range(0, 5) == 0)
        {
            GameObject monsterObj = Instantiate(monsterData[1].prefab, path.points[0].position, Quaternion.identity);

            if (monsterObj.TryGetComponent<MonsterBase>(out MonsterBase monster))
            {
                monster.Initialize(monsterData[1], path.GetPath());
            }
        }
        else
        {
            //경로의 첫 번째 위치에 몬스터 프리팹을 생성
            GameObject monsterObj = Instantiate(monsterData[0].prefab, path.points[0].position, Quaternion.identity);
            
            //존재하면
            if(monsterObj.TryGetComponent<MonsterBase>(out MonsterBase monster))
            {
                monster.Initialize(monsterData[0], path.GetPath());
            }
        }

    }
}
