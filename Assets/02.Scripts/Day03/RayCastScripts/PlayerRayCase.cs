using UnityEngine;

public class PlayerRayCase : MonoBehaviour
{
    //Enemy Rigidbody
    private Rigidbody targetRigid;

    private Vector3 dir;
    private float force = 3.0f;

    private void Awake()
    {
        targetRigid = GetComponent<Rigidbody>();
    }
    void Update()
    {
        Debug.DrawRay(transform.position, transform.forward * 5.0f, Color.red);

        RaycastHit raycastHit;
        //transform.positon: 본인을 의미

        //첫 번째 매개변수 : 시작점 (어디서 쏠거냐)
        //두 번째 매개변수 : 방향(어디방향으로 쏠거냐)
        //세 번째 매개변수 : 충돌정보가 담길 raycastHit
        //네 번째 매개변수 : 최대거리
        if(Physics.Raycast(transform.position, transform.forward, out raycastHit, 5.0f))
        {
            targetRigid = raycastHit.rigidbody;
            dir = transform.forward;
            Debug.Log(raycastHit.collider.gameObject.name);

            if(raycastHit.collider.TryGetComponent<Renderer>(out Renderer renderer)) //안정적이다
            {
                renderer.material.color = Color.red;
            }
        }
    }

    private void FixedUpdate()
    {
        if(targetRigid != null)
        {
            //물리엔진에 힘을 가해서 움직이게 하는 메서드
            targetRigid.AddForce(dir * force, ForceMode.Impulse);
        }
    }
}
