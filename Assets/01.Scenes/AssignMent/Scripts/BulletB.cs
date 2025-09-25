using UnityEngine;

public class BulletB : MonoBehaviour
{
    private float moveSpeed = 8.0f;
    private Rigidbody bulletRigid;

    void Awake()
    {
        bulletRigid = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        Destroy(gameObject, 4.0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyA"))
        {
            if (other.TryGetComponent(out EnemyA enemyA))
            {
                enemyA.Die();
                Destroy(gameObject);
            }
        }
    }

    public void Shot(Vector3 dir, float speed)
    {
        moveSpeed = speed;
        bulletRigid.velocity = dir * moveSpeed;
    }
}
