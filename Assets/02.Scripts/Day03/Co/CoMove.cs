using System.Collections;
using UnityEngine;

public class CoMove : MonoBehaviour
{
    private float height = 2.0f; //������ ����
    private float duration = 1.0f; //�����̴µ� �ɸ��� �ð�

    private Vector3 startPos; //���� ��ġ
    private Vector3 endPos; //��ǥ ��ġ

    private float elapsedTime = 0.0f; //������� �帥 �ð�
    private bool movingUp = true; //true�� ����, false�� �Ʒ���
    void Start()
    {
        ////���� ������ ��ġ ����
        //startPos = transform.position;
        ////��ǥ ��ġ ����
        //endPos = startPos + new Vector3(0.0f, height, 0.0f);

        StartCoroutine(TopDownCubeCo());
    }

    void Update()
    {
        //elapsedTime += Time.deltaTime;
        ////����
        //if (movingUp)
        //{
        //    //���� -> ��ǥ �������� ����ð� ������ ���� ��������
        //    transform.position = Vector3.Lerp(startPos, endPos, elapsedTime / duration); //����
        //}
        ////�Ʒ���
        //else
        //{
        //    transform.position = Vector3.Lerp(endPos, startPos, elapsedTime / duration);
        //}
        ////�̵��ð��� duration�� ������ ������ ����
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
