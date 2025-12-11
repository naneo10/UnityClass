using UnityEngine;

//실제로 행동을 수행하는 코드
public class BT_Leaf : BT_Node
{
    private System.Func<BT_NodeStatus> action; //(델리게이트) 실행할 행동을 저장할 변수

    public BT_Leaf(System.Func<BT_NodeStatus> action)
    {
        this.action = action; //전달받은 함수를 저장
    }

    public override BT_NodeStatus Evaluate()
    {
        return action(); //상태를 리턴
    }
}
