using System.Collections;
using UnityEngine;

public class CoMove : MonoBehaviour
{
    private float height = 2.0f; //움직일 높이
    private float duration = 1.0f; //움직이는데 걸리는 시간

    private Vector3 startPos; //시작 위치
    private Vector3 endPos; //목표 위치

    private float elapsedTime = 0.0f; //현재까지 흐른 시간
    private bool movingUp = true; //true면 위로, false면 아래로
    void Start()
    {
        ////시작 시점의 위치 저장
        //startPos = transform.position;
        ////목표 위치 저장
        //endPos = startPos + new Vector3(0.0f, height, 0.0f);

        StartCoroutine(TopDownCubeCo());
    }

    void Update()
    {
        //elapsedTime += Time.deltaTime;
        ////위로
        //if (movingUp)
        //{
        //    //시작 -> 목표 방향으로 경과시간 비율에 따라 선형보관
        //    transform.position = Vector3.Lerp(startPos, endPos, elapsedTime / duration); //보관
        //}
        ////아래로
        //else
        //{
        //    transform.position = Vector3.Lerp(endPos, startPos, elapsedTime / duration);
        //}
        ////이동시간이 duration을 지나면 방향을 변경
        //if(elapsedTime >= duration)
        //{
        //    elapsedTime = 0.0f;
        //    movingUp = !movingUp;
        //}
    }
    IEnumerator TopDownCubeCo()
    {
        Vector3 startPos = transform.position;
        Vector3 endPos = startPos + Vector3.up * height;

        while (true)
        {
            yield return StartCoroutine(MoveCubeCo(transform, startPos, endPos, duration));
            yield return StartCoroutine(MoveCubeCo(transform, endPos, startPos, duration));
        }
    }
    IEnumerator MoveCubeCo(Transform tr, Vector3 start, Vector3 end, float duration)
    {
        float elapsedTime = 0.0f;
        while (elapsedTime < duration)
        {
            tr.position = Vector3.Lerp(start, end, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}
