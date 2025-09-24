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
        //transform.positon: ������ �ǹ�

        //ù ��° �Ű����� : ������ (��� ��ų�)
        //�� ��° �Ű����� : ����(���������� ��ų�)
        //�� ��° �Ű����� : �浹������ ��� raycastHit
        //�� ��° �Ű����� : �ִ�Ÿ�
        if(Physics.Raycast(transform.position, transform.forward, out raycastHit, 5.0f))
        {
            targetRigid = raycastHit.rigidbody;
            dir = transform.forward;
            Debug.Log(raycastHit.collider.gameObject.name);

            if(raycastHit.collider.TryGetComponent<Renderer>(out Renderer renderer)) //�������̴�
            {
                renderer.material.color = Color.red;
            }
        }
    }

    private void FixedUpdate()
    {
        if(targetRigid != null)
        {
            //���������� ���� ���ؼ� �����̰� �ϴ� �޼���
            targetRigid.AddForce(dir * force, ForceMode.Impulse);
        }
    }
}
