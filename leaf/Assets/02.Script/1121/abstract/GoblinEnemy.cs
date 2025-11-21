using UnityEngine;

public class GoblinEnemy : EnemyBase
{
    [SerializeField] private float patrolDistance = 3.0f;

    private Vector3 startPos;
    private int moveDir = 1; //방향으로 설정하는 변수 1은 오른쪽 음수는 왼쪽

    protected override void Start()
    {
        base.Start(); //공통기능 먼저 실행
        startPos = transform.position; //고블린만의 추가 기능
    }

    void Update()
    {
        //부모클래스 move를 사용하기 위해 이렇게 작성
        //음수 왼쪽, 양수 오른쪽
        float offsetX = transform.position.x - startPos.x; //translate로 처리해오 된다

        //방향 전환
        if (offsetX > patrolDistance)
        {
            moveDir = -1;
        }
        else if (offsetX < -patrolDistance)
        {
            moveDir = 1;
        }

        Vector3 dir = new Vector3(moveDir, 0.0f, 0.0f);

        Move(dir);
    }

    public override void Attack()
    {
        //throw new System.NotImplementedException();
        Debug.Log("고블린이 단검으로 공격");
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 center = transform.position;
        Gizmos.DrawLine(center + Vector3.left * patrolDistance, center + Vector3.right * patrolDistance);
    }
}
