using UnityEngine;


//이 클래스는 상태패턴에서 사용될 추상클래스임.
//적이 가질수 있는 다양한 행동상태의 공통 부모 클래스
//적이 지금 어떤행동을 하고 있는지 하나의 상태로 관리하기 위해 사용
public abstract class EnemyState
{
    protected EnemyController enemy;


    //EnemyState가 생성될때 어떤 EnemyController에 연결할지 지정
    //예) new ChaseState(this)형태로 EnemyController가 넘겨짐
    protected EnemyState(EnemyController enemy)
    {
       this.enemy = enemy;  
    }

    public abstract StateEnums StateType { get; }

    public virtual void Enter() { }
    public virtual void Exit() { }
    
    public virtual void UdpateState() { }

    public virtual void FixedUpdateState() { }


}
