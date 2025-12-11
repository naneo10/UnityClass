using System.Collections.Generic;
using UnityEngine;

/*
여러 자식행동을 순서대로 실행해야 할 때 사용하는 노드
적 앞으로 이동 -> 공격 사거리 인지 확인 -> 공격

원리
1.첫 번째 자식부터 차례대로 Evaluate() 실행
2.자식중 하나라도 Failure -> 즉시 Failure
3.자식중 하나라도 Running -> Sequence도 Running
4.모든 자식이 Success -> Sequence도 Success / Sequence를 비유를 하자면 퀘스트로 비교할 수가 있다
*/
public class BT_Sequence : BT_Node
{
    //시퀀스가 관리할 자식노드 목록
    private List<BT_Node> children;

    public BT_Sequence(List<BT_Node> children) //실행할 자식 리스트를 외부에서 받아서 저장
    {
        this.children = children;
    }

    public override BT_NodeStatus Evaluate() //자식 노드들이 순서대로 실행되면서 결과를 판단 / 순서대로 검사
    {
        //모든 자식 노드를 순서대로 검사
        foreach (var node in children)
        {
            //자식 노드를 실행하고 상태를 받아온다.
            var status = node.Evaluate();

            //1.자식 노드중 하나라도 실패!
            //-> 남은 행동들은 더 이상 실행할 필요가 없음
            //-> 전체 시퀀스는 실패 처리

            if (status == BT_NodeStatus.Failure)
            {
                return BT_NodeStatus.Failure;
            }
            //2.자식 노드가 실행중 이라면
            //->시퀀스도 실행중으로 남아야 됨
            else if (status == BT_NodeStatus.Running)
            {
                return BT_NodeStatus.Running;
            }
        }
        //3.모든 자식 노드가 Success!!!
        //시퀀스 전체가 성공
        return BT_NodeStatus.Success;
    }
}
