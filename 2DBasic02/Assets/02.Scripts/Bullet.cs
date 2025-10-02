using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float speed = 10.0f; //총알이 날아가는 속도
    [SerializeField] float lifeTime = 3.0f; //살아있는 시간
    [SerializeField] float maxY = 6.0f;
    [SerializeField] int damage = 3;

    private float spawnTime; //총알 생성 시점을 기록하는 용도

    public Effect effectPrefab; //총알이 적에 맞았을 때 보여줄 이펙트

    public int Damage
    {
        get { return damage; }
        private set { damage = value; }
    }

    //총알이 활성화 될 때 (풀에서 꺼내올 때)
    private void OnEnable()
    {
        spawnTime = Time.time; //현재시간을 저장
    }

    void Update()
    {
        //총알이 위쪽 방향으로 이동
        transform.Translate(Vector3.up * speed * Time.deltaTime);

        //일정시간 지나면 풀로 돌리자
        if(Time.time - spawnTime >= lifeTime)
        {
            ReturnPool();
        }
        //총알이 화면 위로 벗어나면
        if(transform.position.y > maxY)
        {
            ReturnPool();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("enemy")) return;

        //이펙트 프리팹을 들고온다
        var fx = PoolManager.Instance.GetFromPool(effectPrefab);

        if (fx != null)
        {
            fx.transform.position = transform.position; //총알 위치에 이펙트 배치
            fx.PlayEffect(); //이펙트 재생
        }
        ReturnPool();
    }

    void ReturnPool()
    {
        /*
        if(Managers.Pool != null)
        {
            Managers.Pool.ReturnPool(this);
        }
        */ //다른 방법

        if(PoolManager.Instance != null) //풀매니저가 존재하면
        {
            PoolManager.Instance.ReturnPool(this); //내 자신을 풀로 돌려라
        }
    }
}
