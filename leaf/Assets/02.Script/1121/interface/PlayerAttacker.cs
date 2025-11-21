using UnityEngine;

public class PlayerAttacker : MonoBehaviour
{
    [SerializeField] private int damage = 10;
    [SerializeField] private float attackRange = 2.5f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }
    }

    private void Attack()
    {
        Ray ray = new Ray(transform.position, transform.forward);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, attackRange))
        {
            IDamageble target = hit.collider.GetComponent<IDamageble>();

            if (target != null)
            {
                target.TakeDamage(damage);
                Debug.Log("공격 성공! 데미지를 줬다");
            }
            else
            {
                Debug.Log("떄리긴 했지만 데메지를 받는 놈이 아님");
            }
        }
        else
        {
            Debug.Log("공격 범위에 아무것도 없다");
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(
            transform.position,
            transform.position + transform.forward * attackRange
            );
        Gizmos.DrawWireSphere(transform.position + transform.forward * attackRange, 0.1f);
    }
}
