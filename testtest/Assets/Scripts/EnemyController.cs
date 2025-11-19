using UnityEngine;
using UnityEngine.UIElements;

public class EnemyController : MonoBehaviour
{

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 3.0f;
    [SerializeField] private float rotateSpeed = 8.0f;

    //추적이랑 관련된 것들
    [Header("ChaseInfo")]
    [SerializeField] private float patrolRange = 6.0f;
    [SerializeField] private float detectRange = 10.0f;
    [SerializeField] private float attackRange = 2.0f;

    [Header("Attack")]
    [SerializeField] private float attackDelay = 0.8f;
    [Header("Animator")]
    [SerializeField] Animator animator;

    [Header("Target")]
    [SerializeField] Transform target;


    EnemyState currentState;
    private Rigidbody rigid;

    private Vector3 originPos;
    private Vector3 patrolPos;
    private float nextAttackTime;
    private bool isDead;


    const string animBlend = "Blend";
    const string animAttack = "Attack";


    //상태 객체들
    //외부에서 읽기만 가능
    public EnemyState PatrolState { get; private set; } 
    public EnemyState ChaseState { get; private set; }  
    public EnemyState AttackState { get; private set; } 

    //현재 상태가 어떤 종류인지
    public StateEnums CurrentStateType { get; private set; }

    //외부에서 읽을수 있게 프로퍼티로..
    public float PatrolRange => patrolRange;
    public Transform Target => target;
    public float DetectRange => detectRange;    
    public float AttackRange => attackRange;
    public Vector3 OriginPos => originPos;

    public Vector3 PatrolPos
    {
       get { return patrolPos; }
       set { patrolPos = value; }
    }
    private void Awake()
    {
        rigid = GetComponent<Rigidbody>(); 
        
        if(target==null)
        {
            GameObject go = GameObject.FindGameObjectWithTag("Player");
            if (go != null) target = go.transform;
        }

        originPos = transform.position;

        SetPatrolPoint();

        PatrolState = new PatrolState(this);
        ChaseState = new ChaseState(this);
        AttackState = new AttackState(this);

        ChangeState(PatrolState);
    }


    void Update()
    {
        if (isDead) return;

        //현재 상태에 맞는UdpateState를 실행ㄹ
        currentState.UdpateState();

        UpdateAnimation();
    }
    private void FixedUpdate()
    {
        if (isDead) return;
        currentState.FixedUpdateState();    
    }
    //현재 상태를 다른 상태로 바꿔주는 메서드

    public void ChangeState(EnemyState newState)
    {
        if (currentState == newState) return;

        currentState?.Exit();

        currentState = newState;

        CurrentStateType = newState.StateType;

        currentState.Enter();

    }
    //플레이어까지의 거리를 구해야함.
    public float DistanceToPlayer()
    {
        if(target==null)return Mathf.Infinity;

        return Vector3.Distance(transform.position, target.position);
    }
    //에니메이션 업데이트
    void UpdateAnimation()
    {
        if (animator == null) return;
        float speed = new Vector2(rigid.velocity.x, rigid.velocity.z).magnitude;

        float blend = speed / Mathf.Max(0.01f, moveSpeed);

        animator.SetFloat(animBlend, blend);

    }
    //특정 위치로 이동하는 게 필요
    public void MoveTo(Vector3 pos)
    {
        Vector3 dir = pos - transform.position;
        dir.y = 0.0f;
        dir.Normalize();

        SetHorizontalVelocity(dir * moveSpeed);

        //이동방향을 바라보도록 회전
        LookTo(dir);
    }
    void SetHorizontalVelocity(Vector3 vel)
    {
        Vector3 vec =  rigid.velocity;
        vec.x = vel.x;
        vec.z = vel.z;
        rigid.velocity = vec;
    }

    //주어진 방향(dir)을 향하도록 적을 회전시켜야 함.
    public void LookTo(Vector3 dir)
    {
        if (dir.sqrMagnitude < 0.0001f) return;

        Quaternion targetRot = Quaternion.LookRotation(dir, Vector3.up);

        Quaternion newRot = Quaternion.Slerp(
            rigid.rotation,
            targetRot,
            rotateSpeed * Time.fixedDeltaTime
            );
        rigid.MoveRotation(newRot);
    }
    //공격
    public void Attack()
    {
        if (Time.time < nextAttackTime) return;

        nextAttackTime = Time.time + attackDelay;

        animator?.SetTrigger(animAttack);
    }
    //패트롤 하는거 잇어야함.
    public void SetPatrolPoint()
    {
        Vector2 rand = Random.insideUnitCircle * PatrolRange;

        Vector3 basePos = originPos;
        basePos.y = 0.0f;

        patrolPos = basePos + new Vector3(rand.x,0.0f,rand.y);
    }
    private void OnDrawGizmos()
    {
        Color patrolColor = new Color(0.0f, 0.6f, 1.0f, 0.3f);
        Color chaseColor = new Color(1.0f, 0.6f, 0.0f, 0.3f);
        Color attackColor = new Color(1.0f, 0.0f, 0.0f, 0.3f);

        switch (CurrentStateType)
        {
            case StateEnums.Patrol:
                Gizmos.color = patrolColor;
                break;
            case StateEnums.Chase:
                Gizmos.color = chaseColor;
                break;
            case StateEnums.Attack:
                Gizmos.color = attackColor;
                break;
        }

        Gizmos.DrawSphere(transform.position + Vector3.up * 0.2f, 0.5f);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);

        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, patrolRange);

        Gizmos.DrawSphere(originPos + Vector3.up * 0.1f, 0.2f);

        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(patrolPos + Vector3.up * 0.2f, 0.2f);

    }
}
