using UnityEngine;

public class ChaseState : EnemyState
{
    public ChaseState(EnemyController enemy) : base(enemy) { }

    public override StateEnums StateType
    {
        get { return StateEnums.Chase; }
    }

    public override void UpdateState()
    {
        //타겟이 없으면 다시 패트롤 상태로 돌아감
        if (enemy.Target == null)
        {
            enemy.ChangeState(enemy.PatrolState);
            return;
        }

        float dist = enemy.DistanceToPlayer();
        //공격 가능한 거리안에 들어왔으면 공격 상태로 전환
        if (dist <= enemy.AttackRange)
        {
            enemy.ChangeState(enemy.AttackState);
            return;
        }

        //너무 멀리 도망가면 추적 중단 후 패트롤 상태로 복귀
        if (dist > enemy.DetectRange * 1.3f)
        {
            enemy.ChangeState(enemy.PatrolState);
        }
    }

    public override void FixedUpdateState()
    {
        //타겟이 존재하면
        if (enemy.Target != null)
        {
            //타겟을 향해 이동
            enemy.MoveTo(enemy.Target.position);
        }
    }
}
