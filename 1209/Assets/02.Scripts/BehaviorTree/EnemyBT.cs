using UnityEngine;
using System.Collections.Generic;

public class EnemyBT : MonoBehaviour
{
    //행동트리의 시작점
    private BT_Node root;

    public Transform target;
    public float chaseDistance = 5.0f;
    public float attackDistance = 2.5f;
    public float speed = 2.0f;

    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        //root = 최상단 노드
        root = new BT_Selector(new List<BT_Node>
        {
            //1.공격관련 시퀀스
            new BT_Sequence(new List<BT_Node>
            {
                new BT_Leaf(CheckPlayerInRange),
                new BT_Leaf(AttackPlayer),
            }),
            //2.추적관련 시퀀스
            new BT_Sequence(new List<BT_Node>
            {
                new BT_Leaf(CheckChaseRange),
                new BT_Leaf(ChasePlayer),
            }),
            /*
            확장시 여기에 추가 
            */
            new BT_Leaf(Idle)
        });
    }

    void Update()
    {
        root.Evaluate();
    }

    //범위체크
    private BT_NodeStatus RangeCheck(float range)
    {
        float distance = Vector3.Distance(transform.position, target.position);

        return distance < range ? BT_NodeStatus.Success : BT_NodeStatus.Failure;
    }

    //추격거리 안에 있는지
    BT_NodeStatus CheckChaseRange()
    {
        return RangeCheck(chaseDistance);
    }

    //공격 거리 안에 있는지
    BT_NodeStatus CheckPlayerInRange()
    {
        return RangeCheck(attackDistance);
    }

    //Leaf 행동들 (실제 애니/이동로직)
    BT_NodeStatus Idle()
    {
        AnimatorChange("IDLE");
        return BT_NodeStatus.Success;
    }

    BT_NodeStatus AttackPlayer()
    {
        Rotate();
        AnimatorChange("ATTACK");
        return BT_NodeStatus.Success;
    }

    BT_NodeStatus ChasePlayer()
    {
        Rotate();
        AnimatorChange("MOVE");
        return BT_NodeStatus.Running;
    }

    //animation 상태 변경
    private void AnimatorChange(string temp)
    {
        animator.SetBool("IDLE", false);
        animator.SetBool("MOVE", false);
        animator.SetBool("ATTACK", false);

        animator.SetBool(temp, true);
    }

    private void Rotate()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        Vector3 direction = (target.position - transform.position).normalized;
        direction.y = 0.0f;
        if (direction == Vector3.zero) return;
        transform.forward = direction;
    }
}
