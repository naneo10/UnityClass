using UnityEngine;

//행동트리에서 행동의 결과를 의미하는 상태값
//행동트리의 노드들은 항상 내 행동일 잘됐냐? 결과를 알려줘야함
public enum BT_NodeStatus
{
    Success, //행동이 성공적으로 끝났을 때
    Failure, //행동을 할 수 없거나 조건이 맞지 않아서 실패했을 때
    Running //행동이 아직 끝나지 않고 진행중일 때
}

//행동트리의 모든 노트의 공통 부모 클래스
//행동트리는 여러종류의 노드로 구성된다.
//특히 모든 노드들은 반드시 평가를 가져야한다.
public abstract class BT_Node
{
    public abstract BT_NodeStatus Evaluate();
}
