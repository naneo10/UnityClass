using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [Header("설정")]
    //[SerializeField] private Item 
    [SerializeField] private float spawnInterval = 2.0f; //스폰 간격
    [SerializeField] private float spawnOffsetX = 25.0f; //좌우 스폰 위치
    [SerializeField] private float spawnOffsetZ = 25.0f; //전후 스폰 위치
    [SerializeField] private float spawnY = 2.0f; //스폰될 y좌표
    public int itemTotalCount = 17; //아이템 총 갯수

    private void Start()
    {
        
    }

    private void SetupItems()
    {
        for (int i = 0; i < itemTotalCount; i++)
        {
            //var item = Instantiate()
        }
    }
}