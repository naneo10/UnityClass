using UnityEngine;
/*
[Layer]
-�� ������Ʈ�� � �׷쿡 ���ϴ��� ����
-��Ʈ�����̶� 32��Ʈ Ÿ���� int���� ���̾� ���� 32���� ����
[LayerMask]
-Ư�� ���̾ ��󳻼� �˻��� �� ���
-����ĳ��Ʈ���� Ư�����̾ ���߰� ���� �� ���
-���̾ �ߺ����� ���� �����ϴ�
-��Ʈ����ũ ���� ������
-�ױ״� ���ڿ�
*/
public class layerBasic : MonoBehaviour
{
    private float distance = 10.0f;
    public LayerMask targetMask;
    void Update()
    {
        Debug.DrawRay(transform.position, transform.forward * distance, Color.green);

        if(Physics.Raycast(transform.position, transform.forward, out RaycastHit raycastHit, distance, targetMask))
        {
            if(raycastHit.collider.TryGetComponent<Renderer>(out Renderer renderer))
            {
                renderer.material.color = Color.red;
            }
        }
    }
}
