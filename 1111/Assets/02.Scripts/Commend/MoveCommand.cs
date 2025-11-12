using UnityEngine;

/*
플레이어의 이동명령을 하나의 객체로 표현하는 클래스
커맨드 패턴을 이용해서 실행, 되돌리기를 쉽게 관리할 수 있음
*/
public class MoveCommand : ICommand
{
    private PlayerMover m_PlayerMover;
    private Vector3 m_MoveVector;

    //생성자
    //moveCommand가 만들어질때 어떤 플레이어를 어떤 방향으로 이동시킬지 정보를 받아 저장
    public MoveCommand(PlayerMover player, Vector3 moveVector)
    {
        m_PlayerMover = player;
        m_MoveVector = moveVector;
    }

    public void Execute()
    {
        //1.이동 후의 위치를 경로 표시용에 추가
        m_PlayerMover.playerPath.AddToPath(m_PlayerMover.transform.position + m_MoveVector);

        //2.플레이어를 실제로 이동시킴
        m_PlayerMover.Move(m_MoveVector);
    }

    public void Undo()
    {
        //1.반대 방향으로 이동
        m_PlayerMover.Move(-m_MoveVector);

        //2.이동경로에서 마지막 점을 제거
        m_PlayerMover.playerPath.RemoveFromPath();
    }
}