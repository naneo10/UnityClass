using UnityEngine;

public class EventMethod : MonoBehaviour
{
    /*
    1.유니티 생명주기 사이클 : https://docs.unity3d.com/2022.2/Documentation/Manual/ExecutionOrder.html
    2.awake vs start 차이점
    3.박스 이동하면서 회전할 때 진행 방향에 맞춰서 회전하기
    쿼터니언
    부드럽게 회전하게 하는 메서드
    */
    private void Awake()
    {
        Debug.Log("Awake 호출");
    }

    void Start()
    {
        Debug.Log("Start 호출");
    }

    void Update()
    {
        Debug.Log("Update 호출");
    }

    private void FixedUpdate()
    {
        Debug.Log("FixedUpdate 호출");
    }

    private void LateUpdate()
    {
        Debug.Log("Latedate 호출");
    }

    private void OnEnable()
    {
        Debug.Log("OnEnable 호출");
    }

    private void OnDisable()
    {
        Debug.Log("OnDisable 호출");
    }
}
