using UnityEngine;
/*
[RayCastAll]
-하나의 레이로 부딪히는 모든 컬라이더를 배열로 반환
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
