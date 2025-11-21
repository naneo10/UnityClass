using UnityEngine;

public class CEnemyShooter : ShootBase
{
    [SerializeField] private float startDelay = 1.0f;
    private float spawnTime;

    void Start()
    {
        spawnTime = Time.time;
    }

    void Update()
    {
        if (Time.time - spawnTime < startDelay) return;
        Fire();
    }
}