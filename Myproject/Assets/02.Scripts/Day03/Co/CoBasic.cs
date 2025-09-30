using System.Collections;
using UnityEngine;
/*
[코루틴]
-유니티에서 코루틴을 실행을 일시정지 하고 제어를 유니티에 반환하지만
중단한 부분에서 다음 프레임을 계속 할 수 있는 메서드
-코루틴은 유니티에서 시간에 따라 작업을 수행할 때 유용
-일반적인 메서드는 호출되면 즉시 실행이 끝남. 코루틴을 특정 조건(시간, 프레임 ...)을 만족할 때 까지 실행을 멈출 수 있음

[특징]
-IEnumerator을 반환하는 메서드로 작성
-yield 키워드를 사용하여 실행을 일시중지하고 특정 조건이 충족되면 다시 실행
-StartCoroutine()을 사용하여 실행하고 StopCoroutine() 또는 StopAllCoroutine()을 사용하여 중단

[yield]
-yield return 값; -> 현재 값을 반환하고 실행을 일시중지 후 다음 호출시 재개
-yield break; -> 반복 또는 코루틴을 즉시 종료
-yield return null; -> 다음 프레임까지 대기
-yield return new WaitForSeconds(t); -> t초 동안 대기
-yield return new WaitUntil(조건); -> 특정 조건이 true가 될 때까지 대기
-yield return new WaitWhile(조건); -> 특정 조건이 false가 될 떄까지 대기
-yield return StartCoroutine(코루틴) -> 다른 코루틴이 끝난 후 실행
*/
public class CoBasic : MonoBehaviour
{
    private int count = 0;

    private float timer = 0.0f;

    private void Start()
    {
        //StartCoroutine(PrintNumberCo());
        StartCoroutine(PrintDelay());
    }

    void Update()
    {
        //if (count < 5)
        //{
        //    Debug.Log(count);
        //    count++;
        //}

        //if(count < 3)
        //{
        //    timer += Time.deltaTime;
        //    {
        //        if(timer >= 1.0f)
        //        {
        //            Debug.Log(count);
        //            count++;
        //            timer = 0.0f;
        //        }
        //    }
        //}
    }

    IEnumerator PrintNumberCo()
    {
        for(int i = 0; i < 5; i++)
        {
            Debug.Log(i);
            yield return null;
        }
    }

    IEnumerator PrintDelay()
    {
        for(int i = 0; i < 3; i++)
        {
            Debug.Log(i);
            yield return new WaitForSeconds(1.0f); //1초 후 대기 실행
        }
    }
}
