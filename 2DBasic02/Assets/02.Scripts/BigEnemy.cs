using UnityEngine;

public class BigEnemy : Enemy
{
    private Vector2 initSpawnPosition;
    private float respawnTime = 3.0f;

    //움직이는 스크립트 있어야 됨
    private EnemyMovement movement;

    //스폰하는거 있어야 함
    private EnemySpawner spawner;

    [SerializeField] float deSpawnY = -6.0f;

    protected override void Awake()
    {
        base.Awake();
        movement = GetComponent<EnemyMovement>();
    }

    public void Init(Vector2 spawnPosition, EnemySpawner spawner)
    {
        initSpawnPosition = spawnPosition;

        //실제 위치를 스폰된 위치로 옮김
        transform.position = initSpawnPosition;

        this.spawner = spawner;

        //이동 관련
        movement.SetRandomPattern();
        movement.ResetMoveMent();
    }

    void Update()
    {
        movement.MoveEnemy(initSpawnPosition);

        //만약 적이 화면 아래로 내려가면 (deSpawnY보다 더 아래이면)
        if (transform.position.y < deSpawnY)
        {
            gameObject.SetActive(false); //2.끄고

            //1.스포너가 있으면
            if (spawner != null )
            {
                //3.스포너한태 재요청
                spawner.RequestRespawn(initSpawnPosition, respawnTime);
            }
        }
    }
}
