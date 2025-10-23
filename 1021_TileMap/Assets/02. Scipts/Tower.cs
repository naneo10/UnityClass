using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [Header("bullet setting")]
    [SerializeField] private GameObject[] bulletPrefab;
    [SerializeField] private Transform firePoint;

    [Header("attack setting")]
    [SerializeField] private float range = 3.0f;
    [SerializeField] private float fireRate = 1.0f;

    private float fireTimer;
    private List<MonsterBase> monsterInRange = new List<MonsterBase>();

    private int weaponIndex = 0;

    void Update()
    {
        fireTimer += Time.deltaTime;

        MonsterBase target = GetNearMonster();

        if (target == null) return;

        Vector2 dir = target.transform.position - transform.position;
        transform.right = dir;

        if (fireTimer >= 1.0f / fireRate)
        {
            Fire(target);
            fireTimer = 0.0f;
        }
    }

    //
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<MonsterBase>(out MonsterBase monster))
        {
            //목록에 없으면 추가
            if (!monsterInRange.Contains(monster))
            {
                monsterInRange.Add(monster);
            }
        }
    }

    //사거리 안에 저장된 몬스터 중에서 가장 가까운 몬스터를 찾아야함
    private MonsterBase GetNearMonster()
    {
        if (monsterInRange.Count == 0) return null;

        MonsterBase nearMonster = null;

        float nearDist = Mathf.Infinity; //현재까지의 가장 짧은 거리
        Vector2 towerPos = transform.position; //타워 위치

        //사거리 안에 모든 몬스터를 하나씩 확인
        foreach (var monster in monsterInRange)
        {
            if (monster == null) continue;

            //타워 - 몬스터 거리
            float dist = Vector2.Distance(towerPos, monster.transform.position);

            /*
            즉 현재 보고 있는 몬스터가 지금까지 본 것 보다 가까우면
            해당 몬스터를 가장 가까운 몬스터로 바꾸자

            if (현재거리 < 지금까지 가장 짧은 거리)
            지금까지 가장 짧은 거리 = 현재 거리
            가장 가까운 몬스터 = 이번 몬스터
            */
            if(dist < nearDist)
            {
                nearDist = dist;
                nearMonster = monster;
            }
        }
        return nearMonster;
    }

    private void Fire(MonsterBase target)
    {
        GameObject bullet = Instantiate(bulletPrefab[weaponIndex], firePoint.position, firePoint.rotation);

        bullet.GetComponent<Bullet>().Initialize(target);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    public void Upgrade()
    {
        weaponIndex += 1;
        if (weaponIndex >= bulletPrefab.Length)
        {
            weaponIndex = bulletPrefab.Length - 1;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<MonsterBase>(out MonsterBase monster))
        {
            if (monsterInRange.Contains(monster))
            {
                monsterInRange.Remove(monster);
            }
        }
    }
}
