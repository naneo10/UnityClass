using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [Header("프리팹")]
    [SerializeField] private ItemTypeA itemPrefab;
    public Transform center;

    [Header("설정")]
    [SerializeField] private float spawnInterval = 1.5f; //스폰 간격
    [SerializeField] private float spawnOffset = 25.0f; //스폰 위치
    [SerializeField] private float spawnY = 2.0f; //스폰될 y좌표
    public int itemTotalCount = 17; //아이템 총 갯수

    private float timer;

    private List<ItemTypeA> items = new List<ItemTypeA>();

    private void Awake()
    {
        if (center == null)
        {
            center = this.transform;
        }

        SetupItems();
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

        for (int i = 0; i < itemTotalCount; i++)
        {
            Vector2 rand = Random.insideUnitSphere * spawnOffset;
            Vector3 pos = center.position + new Vector3(rand.x, spawnY, rand.y);

            var item = Instantiate(itemPrefab, pos, Quaternion.identity);
            item.gameObject.SetActive(false);
            items.Add(item);
        }
    }

    //private ItemTypeA GetFromItem()
    //{
    //    for (int i = 0; i < items.Count; i++)
    //    {
    //        var item = items[i];

    //        if (item != null && item.gameObject.activeInHierarchy)
    //        {
    //            return item;
    //        }
    //    }

    //    var newItem = 
    //}
}