using UnityEngine;

public abstract class MonsterBase : MonoBehaviour
{
    [Header("monster stat")]
    protected int currentHp;
    protected float moveSpeed;

    [Header("path setting")]
    protected Transform[] path;
    protected int targetIndex;
    private float arriveRange = 0.1f; //허용 오차 이쯤이면 도착했다

    public virtual void Initialize (MonsterData data, Transform[] pathPoints)
    {
        currentHp = data.maxHp;
        moveSpeed = data.moveSpeed;
        path = pathPoints;
        targetIndex = 0;
    }

    protected virtual void Update()
    {
        MovePath();
    }

    //경로에 저장된 웨이포인트들을 순서대로 따라 이동
    protected virtual void MovePath()
    {
        if (path == null || targetIndex >= path.Length) return;

        Transform target = path[targetIndex];

        Vector2 currentPos = transform.position;
        Vector2 targetPos = target.position;

        transform.position = Vector2.MoveTowards(currentPos, targetPos, moveSpeed * Time.deltaTime);

        if (Vector2.Distance(currentPos, targetPos) <  arriveRange)
        {
            targetIndex++; //다음 웨이 포인트를 목표로 설정
        }
    }

    public virtual void TakeDamage(int damage)
    {
        currentHp -= damage;
        if (currentHp <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
        GameManager.Instance.AddCount();
    }
}
