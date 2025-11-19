using UnityEngine;

public class itemTypeA : Item
{
    private Vector3 initSpawnPosition;
    private float activeTime = 2.0f;

    //아이템 스포너
    private ItemSpawner spawner;

    protected override void Awake()
    {
        base.Awake();
    }

    public void Init(Vector3 spawnPosition, ItemSpawner spawner)
    {
        initSpawnPosition = spawnPosition;

        transform.position = initSpawnPosition;

        this.spawner = spawner;
    }
}
