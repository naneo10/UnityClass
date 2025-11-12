using UnityEngine;
using System.Collections.Generic;

/*
-실행된 명령들을 관리하고, 되돌리기와 다시실행을 처리하는 클래스
-모든 명령을 스택에 저장해서 이전/다음 명령을 순서대로 관리
*/
public class CommandInvoker
{
    //실행한 명령들을 저장하는 스택
    private static Stack<ICommand> s_UndoStack = new Stack<ICommand>();

    //되돌린 명령들을 저장하는 스택
    private static Stack<ICommand> s_RedoStack = new Stack<ICommand>();

    //새로운 명령(예:이동)을 실행하고 Undo목록에 추가
    public static void ExecuteCommand(ICommand command)
    {
        //1.명령 실행
        command.Execute();

        //2.실행된 명령을 Undo 스택에 저장
        s_UndoStack.Push(command);

        //3.새로운 명령이 실행되면 Redo스택은 초기화
        s_RedoStack.Clear();
    }

    //가장 최근에 실행한 명령을 되돌림
    public static void UndoCommand()
    {
        //실행 취소할 명령이 있다면
        if(s_UndoStack.Count > 0)
        {
            //1.마지막으로 실행된 명령을 꺼내고
            ICommand activeCommand = s_UndoStack.Pop();

            //2.나중에 다시 실행할 수 있도록 Redo스택에 저장
            s_RedoStack.Push(activeCommand);

            //3.명령 되돌리기
            activeCommand.Undo();
        }
        else
        {
            Debug.Log("실행 취소할 명령이 없습니다.");
        }
    }

    //Undo로 되돌린 명령을 다시 실행
    public static void RedoCommand()
    {
        //다시 실행할 명령이 있다면
        if (s_RedoStack.Count > 0)
        {
            //1.Redo 스택에서 명령을 꺼내고
            ICommand activeCommand = s_RedoStack.Pop();

            //2.실행후 다시 Undo스택에 저장(되돌리기 가능하게)
            s_UndoStack.Push(activeCommand);

            //3.명령 다시 실행
            activeCommand.Execute();
        }
        else
        {
            Debug.Log("다시 실행할 명령이 없습니다.");
        }
    }
}