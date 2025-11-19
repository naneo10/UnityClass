
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{

    [Header("프리팹")]
    public GameObject itemPrefab;
    public Transform center;

    [Header("스폰 설정")]
    [SerializeField] float radius = 2.0f;
    [SerializeField] int startSpawncount = 5;
    [SerializeField] int maxCount = 20;
    [SerializeField] float spawnInterval = 1.5f;

    private float timer;

    void Start()
    {
        if (center==null)
        {
            center = transform;
        }

        for(int i = 0; i < startSpawncount; i++)
        {
            ItemSpawn();
        }

    }

    void Update()
    {
        timer += Time.deltaTime;
        if(timer>=spawnInterval)
        {
            timer -= spawnInterval;
            if(CountItem()<maxCount)
            {
                ItemSpawn();
            }
        }
    }

    void ItemSpawn()
    {
        if (itemPrefab == null) return;


        Vector2 rand = Random.insideUnitCircle * radius;
        Vector3 pos = center.position + new Vector3(rand.x, rand.y, 0.0f);

        GameObject go = Instantiate(itemPrefab, pos, Quaternion.identity);

    }
    int CountItem()
    {
        var items = FindObjectsOfType<CItem>();

        return items.Length;
    }

    private void OnDrawGizmos()
    {
        Transform tr;
        if (center != null)
        {
            tr = center;
        }
        else
        {
            tr = transform;
        }
        Gizmos.color = new Color(0.2f, 0.8f, 1.0f, 0.7f);
        Gizmos.DrawWireSphere(tr.position, radius);
    }
}
