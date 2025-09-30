using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("추적 대상")]
    public Transform player;

    [Header("설정")]
    [SerializeField] private float chaseRange = 5.0f;
    [SerializeField] private float moveSpeed = 2.0f;

    void Update()
    {
        Vector2 dir = player.position - transform.position;

        //타겟과 실제거리 < 추적범위
        if(dir.magnitude < chaseRange)
        {
            Vector2 direction = dir.normalized;
            transform.Translate(direction * moveSpeed * Time.deltaTime);
        }
    }

    private void OnDrawGizmos()
    {
        Vector2 enemyPos = transform.position;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(enemyPos, chaseRange);
    }
}
