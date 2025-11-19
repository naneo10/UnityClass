


using UnityEngine;
public class AttackState : EnemyState
{
    public AttackState(EnemyController enemy) : base(enemy) { }

    public override StateEnums StateType
    {
        get { return StateEnums.Attack; }
    }

    public override void UdpateState()
    {
        //타겟이 없으면 순찰로 복귀
        if(enemy.Target==null)
        {
            enemy.ChangeState(enemy.PatrolState);
            return;
        }

        float dist = enemy.DistanceToPlayer();

        //공격 가능거리보다 멀어졌다면
        if(dist> enemy.AttackRange *1.2f)
        {
            //그래도 시야 범위 안에 있으면 추격
            if(dist<=enemy.DetectRange)
            {
                enemy.ChangeState(enemy.ChaseState);
            }
            else
            {
                enemy.ChangeState(enemy.PatrolState);
            }
        }
    }
    public override void FixedUpdateState()
    {
        if (enemy.Target == null) return;


        //1.방향 계산. 플레이어 방향을 계산해서 적이 그쪽을 바라보게 함
        Vector3 dir = enemy.Target.position - enemy.transform.position;
        dir.y = 0.0f;

        //적을 플레이어 방향으로 회전
        enemy.LookTo(dir);

        //공격실행
        enemy.Attack();


    }
}
