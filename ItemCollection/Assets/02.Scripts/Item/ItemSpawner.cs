using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(ItemTypeA))]
public class ItemSpawner : MonoBehaviour
{
    [Header("프리팹")]
    [SerializeField] private ItemTypeA itemPrefab;
    public Transform center;

    [Header("설정")]
    [SerializeField] private float spawnInterval = 1.5f; //스폰 간격
    [SerializeField] private float spawnOffset = 25.0f; //스폰 위치
    [SerializeField] private float spawnY = 2.0f; //스폰될 y좌표

    private ItemTypeA itemTypeA;
    public int spawnCount = 20;

    private float timer;

    private void Awake()
    {
        if (center == null)
        {
            center = this.transform;
        }

        itemTypeA = GetComponent<ItemTypeA>();
        //SceneLoaded 시 파괴되 없어지는 문제 poolmanager 자식으로 옮김으로 해결
        //실패1. itemTypeA 즉 아이템을 싱글톤으로 작성시 20개의 오브젝트가 20개의 중복으로 인식
        //결과1. 중복된 오브젝트 파괴
        Managers.Pool.CreatePool(itemPrefab, spawnCount, PoolManager.Instance.transform);
    }

    private void Start()
    {
        StartCoroutine(AutoSpawnCo());
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            timer -= spawnInterval;
        }
    }

    private void SetupItems()
    {
        if (itemPrefab == null) return;

        itemTypeA = Managers.Pool.GetFromPool(itemPrefab);

        Vector2 rand = Random.insideUnitSphere * spawnOffset;
        Vector3 pos = center.position + new Vector3(rand.x, spawnY, rand.y);

        itemTypeA.transform.SetPositionAndRotation(pos, Quaternion.identity);
        itemTypeA.gameObject.SetActive(true);
    }

    private IEnumerator AutoSpawnCo()
    {
        for (int i = 0; i < spawnCount; i++)
        {
            SetupItems();
            Debug.Log($"쿨타임 : {itemTypeA.coolTime}");
            yield return new WaitForSeconds(itemTypeA.coolTime);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, spawnOffset);
    }
}