using System.Collections;
using UnityEngine;
/*
[�ڷ�ƾ]
-����Ƽ���� �ڷ�ƾ�� ������ �Ͻ����� �ϰ� ��� ����Ƽ�� ��ȯ������
�ߴ��� �κп��� ���� �������� ��� �� �� �ִ� �޼���
-�ڷ�ƾ�� ����Ƽ���� �ð��� ���� �۾��� ������ �� ����
-�Ϲ����� �޼���� ȣ��Ǹ� ��� ������ ����. �ڷ�ƾ�� Ư�� ����(�ð�, ������ ...)�� ������ �� ���� ������ ���� �� ����

[Ư¡]
-IEnumerator�� ��ȯ�ϴ� �޼���� �ۼ�
-yield Ű���带 ����Ͽ� ������ �Ͻ������ϰ� Ư�� ������ �����Ǹ� �ٽ� ����
-StartCoroutine()�� ����Ͽ� �����ϰ� StopCoroutine() �Ǵ� StopAllCoroutine()�� ����Ͽ� �ߴ�

[yield]
-yield return ��; -> ���� ���� ��ȯ�ϰ� ������ �Ͻ����� �� ���� ȣ��� �簳
-yield break; -> �ݺ� �Ǵ� �ڷ�ƾ�� ��� ����
-yield return null; -> ���� �����ӱ��� ���
-yield return new WaitForSeconds(t); -> t�� ���� ���
-yield return new WaitUntil(����); -> Ư�� ������ true�� �� ������ ���
-yield return new WaitWhile(����); -> Ư�� ������ false�� �� ������ ���
-yield return StartCoroutine(�ڷ�ƾ) -> �ٸ� �ڷ�ƾ�� ���� �� ����
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
            yield return new WaitForSeconds(1.0f); //1�� �� ��� ����
        }
    }
}
