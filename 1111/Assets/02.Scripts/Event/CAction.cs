using System;
using UnityEditor.Build.Content;
using UnityEngine;

/*
[Action]
-반환 값이 없는 메서드를 참조하는 델리게이트
-매개변수는 없을 수도 있고 있다면 최대 16개의 인자를 받을 수 있다
*/

public class CAction : MonoBehaviour
{
    //public delegate void MyDelegate(string msg);

    Action Hello; //매개변수 없는
    Action<string> Hello2; //string 1개
    Action<int, int> numbers; //int형 2개

    void HelloPrint()
    {
        Debug.Log("안녕");
    }

    void HelloPrint2(string msg)
    {
        Debug.Log($"말하기 : {msg}");
    }

    void Add(int a, int b )
    {
        Debug.Log($"합계 : {a + b}");
    }

    private Action startActions;
    private void ShowMessage() => Debug.Log("게임시작");
    private void InitPlayer() => Debug.Log("플레이어 초기화 완료");
    private void LoadData() => Debug.Log("데이터 로드 완료");

    private void Awake()
    {
        startActions += ShowMessage;
        startActions += InitPlayer;
        startActions += LoadData;
    }

    void Start()
    {
        //Hello = HelloPrint;
        //Hello2 = HelloPrint2;
        //numbers = Add;

        //등록된 모든 Action 순서대로 실행
        startActions?.Invoke();
    }

    private void OnDestroy()
    {
        startActions -= ShowMessage;
        startActions -= InitPlayer;
        startActions -= LoadData;
    }
}

/* 해제를 하지 않았을 때 예시
public class UIManager : MonoBehaviour
{
    private void OnEnable()
    {
        GameManager.OnGameOver += ShowGameoverUI;
    }

    private void OnDisable()
    {
        GameManager.OnGameOver -= ShowGameoverUI;
    }

    public void ShowGameoverUI()
    {
        
    }
}
*/