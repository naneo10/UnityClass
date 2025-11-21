using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrayBasic02 : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private GameObject enemyPrefabs;

    void Start()
    {
        SpawnEnemies();
    }

    /*
    모든 스폰지점에 적을 생성하는 메서드
    위치가 5군데다
    SpawnPoints 길이가 5
    spawnPoints[0] ~ spawnPoints[4]
    */

    //모든 스폰지점에 적을 생성하는 메서드
    private void SpawnEnemies()
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            //생성
            Instantiate(enemyPrefabs, spawnPoints[i].position, Quaternion.identity);
        }
    }
}
