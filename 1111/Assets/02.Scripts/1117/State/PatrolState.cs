using UnityEngine;

public class PatrolState : EnemyState
{
    private bool isReturn;

    public PatrolState(EnemyController enemy) : base(enemy) { }

    public override StateEnums StateType
    {
        get { return StateEnums.Patrol; }
    }

    public override void Enter()
    {
        //집(OriginPos)에서 얼마나 떨어져있는지 계산
        float distFromOrigin = Vector3.Distance(enemy.transform.position, enemy.OriginPos);

        isReturn = distFromOrigin > enemy.PatrolRange * 1.2f;

        if (!isReturn)
        {
            enemy.SetPatrolPoint();
        }
    }

    public override void UpdateState()
    {
        float dist = enemy.DistanceToPlayer();

        if (enemy.Target != null)
        {
            if ( dist <= enemy.AttackRange)
            {
                enemy.ChangeState(enemy.AttackState);
                return;
            }
            if (dist <= enemy.DetectRange)
            {
                enemy.ChangeState(enemy.ChaseState);
                return;
            }
        }

        if (isReturn)
        {
            float distFromOrigin = Vector3.Distance(enemy.transform.position, enemy.OriginPos);

            //근처까지 도착했으면
            if (distFromOrigin <= enemy.PatrolRange * 0.9f)
            {
                isReturn = false;
                enemy.SetPatrolPoint();
            }
        }
        else
        {
            if (Vector3.Distance(enemy.transform.position, enemy.PatrolPos) < 0.6f)
            {
                enemy.SetPatrolPoint();
            }
        }
    }

    public override void FixedUpdateState()
    {
        if (isReturn)
        {
            enemy.MoveTo(enemy.OriginPos);
        }
        else
        {
            enemy.MoveTo(enemy.PatrolPos);
        }
    }
}
