using UnityEngine;
/*
[Layer]
-이 오브젝트가 어떤 그룹에 속하는지 지정
-비트연산이라 32비트 타입인 int에서 레이어 수가 32개로 제한
[LayerMask]
-특정 레이어만 골라내서 검사할 때 사용
-레이캐스트에서 특정레이어만 맞추고 싶을 때 사용
-레이어를 중복으로 적용 가능하다
-비트마스크 연산 빠르다
-테그는 문자열
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
