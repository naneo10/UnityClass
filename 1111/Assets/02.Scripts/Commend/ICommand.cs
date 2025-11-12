using UnityEngine;

/*
[커맨드 패턴]
-행동을 객체로 캡슐화 하는 디자인 패턴
-실행, 취소(Undo), 재실행(Redo) 같은 기능을 구현할 때 아주 유용

예)'RTS유닛 이동 > 채집 > 복귀' 같은 일련의 행동을 각각 MoveCommand, CollectCommand, ReturnCommand 객체로 저장

장점
-실행, 취소, 재실행이 쉽다 (명령 객체에 행동과 이전상태를 함께 저장할 수 있어 한 번 실행된 행동을 손 쉽게 되돌릴 수 있다.)
-요청과 실행의 분리 (명령을 실행하는 쪽과 실제 동작하는 쪽을 분리해서 코드 의존성을 줄일 수 있다.)
-행동을 데이터 처럼 다루고 싶을 때는 강력하다
*/
public interface ICommand
{
    //실제로 명령을 실행할 때 호출
    public void Execute();

    //방금 실행할 명령을 되돌릴 때 호출
    public void Undo();
}
