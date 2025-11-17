using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrayBasic04 : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private GameObject bulletPrefabs;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SpawnBullet();
        }
    }

    //총알을 하나 스폰시키는 메서드
    private void SpawnBullet()
    {
        Transform spawnPoint = GetRandomSpawnPoint();

        Instantiate(bulletPrefabs, spawnPoint.position, spawnPoint.rotation);
    }

    //랜덤한 스폰 포인트를 반환시키는 메서드 : 트랜스폼을 리턴
    private Transform GetRandomSpawnPoint()
    {
        int index = Random.Range(0, spawnPoints.Length);

        //선택한 인덱스에 해당하는 위치를 반환
        return spawnPoints[index];
    }
}