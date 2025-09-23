using UnityEngine;

public class EventMethod : MonoBehaviour
{
    /*
    1.����Ƽ �����ֱ� ����Ŭ : https://docs.unity3d.com/2022.2/Documentation/Manual/ExecutionOrder.html
    2.awake vs start ������
    3.�ڽ� �̵��ϸ鼭 ȸ���� �� ���� ���⿡ ���缭 ȸ���ϱ�
    ���ʹϾ�
    �ε巴�� ȸ���ϰ� �ϴ� �޼���
    */
    private void Awake()
    {
        Debug.Log("Awake ȣ��");
    }

    void Start()
    {
        Debug.Log("Start ȣ��");
    }

    void Update()
    {
        Debug.Log("Update ȣ��");
    }

    private void FixedUpdate()
    {
        Debug.Log("FixedUpdate ȣ��");
    }

    private void LateUpdate()
    {
        Debug.Log("Latedate ȣ��");
    }

    private void OnEnable()
    {
        Debug.Log("OnEnable ȣ��");
    }

    private void OnDisable()
    {
        Debug.Log("OnDisable ȣ��");
    }
}
