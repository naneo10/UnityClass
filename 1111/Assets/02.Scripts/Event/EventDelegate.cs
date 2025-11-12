using System;
using UnityEngine;

/*
[이벤트]
-일련의 사건이 발생했다는 사실을 다른 객체에게 전달
-대리자 기반의 알림시스템
-UI 프로그래밍이나 게임갭발, 비동기 프로그래밍에서 사용
-옵져버 패턴을 구현하는데 주로 사용

[델리게이트 체인과 이벤트의 차이]
-델리게이트는 여러 메서드를 체인으로 연결할 수 있어 이벤트처럼 동작할 수 있음
-이벤트로 사용하기에는 문제점이 있다
1.대입연산을 통해 기존체인이 모두 초기화가 될 수 있음
2.외부 클래스에서도 직접 호출이 가능
    ㄴ객체 내부에서 이벤트발생 조건이 만족되지 않아도 외부코드가 임의로 인보크할 수 있는 문제
-이벤트 event를 붙히면 대입, 외부호출이 제한됨
    ㄴ안전하게 구독/해제만 가능한 이벤트전용 델리게이트로 사용할 수 있음
*/

public class EventSender
{
    public Action onDelegate; //일반 델리게이트
    public event Action onEvent; //이벤트

    public void EventCall()
    {
        onEvent?.Invoke();
    }

    public void DelegateCall()
    {
        onDelegate?.Invoke();
    }
}

public class EventListener
{
    public void React()
    {
        Debug.Log("반응 실행");
    }
}

public class EventDelegate : MonoBehaviour
{
    void Start()
    {
        var sender = new EventSender();
        var eventListener1 = new EventListener();
        var eventListener2 = new EventListener();
        var eventListener3 = new EventListener();

        sender.onDelegate += eventListener1.React;
        sender.onDelegate += eventListener2.React;
        sender.onDelegate = eventListener3.React; //기존 구독이 모두 사라짐(주의)
        sender.DelegateCall();

        sender.onEvent += eventListener1.React;
        //sender.onEvent = eventListener2.React; 이벤트는 대입이 불가능하다(안전)

        sender.onDelegate?.Invoke(); //델리게이트는 외부에서 직접호출 가능
        //sender.onEvent?.Invoke(); 외부에서 직접호출 불가능
        sender.EventCall(); //이렇게 호출해야 한다
    }
}
