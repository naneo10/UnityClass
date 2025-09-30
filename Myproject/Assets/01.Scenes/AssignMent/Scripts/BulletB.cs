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
        Destroy(gameObject, 5.0f);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyA"))
        {
            if (other.TryGetComponent(out EnemyA enemyA))
            {
                gameObject.SetActive(false);
                enemyA.Kill();
                //Destroy(gameObject);
            }
        }
        if (other.CompareTag("EnemyA"))
        {
            if (other.TryGetComponent(out EnemyB enemyB))
            {
                gameObject.SetActive(false);
                enemyB.Kill();
                //Destroy(gameObject);
            }
        }
    }
    public void Shot(Vector3 dir, float speed)
    {
        moveSpeed = speed;
        bulletRigid.velocity = dir * moveSpeed;
    }
}
