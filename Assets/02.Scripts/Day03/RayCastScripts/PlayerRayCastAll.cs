using UnityEngine;
/*
[RayCastAll]
-�ϳ��� ���̷� �ε����� ��� �ö��̴��� �迭�� ��ȯ
*/
public class PlayerRayCastAll : MonoBehaviour
{
    public LayerMask targetLayer;
    void Update()
    {
        Debug.DrawRay(transform.position, transform.forward * 8.0f, Color.green);

        RaycastHit[] raycastHits = Physics.RaycastAll(transform.position, transform.forward, 8.0f, targetLayer);

        foreach (RaycastHit hit in raycastHits)
        {
            Debug.Log(hit.collider.gameObject.name);

            if(hit.collider.TryGetComponent<Renderer>(out Renderer renderer))
            {
                renderer.material.color = Color.blue;
            }
        }
    }
}
