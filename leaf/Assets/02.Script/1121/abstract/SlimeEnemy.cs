using UnityEngine;

public class SlimeEnemy : EnemyBase
{
    void Update()
    {
        Vector3 dir = Vector3.forward;
        Move(dir);
    }

    public override void Attack()
    {
        Debug.Log("슬라임이 점프공격한다");
    }
}
