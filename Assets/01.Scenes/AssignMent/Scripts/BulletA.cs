using UnityEngine;

public class BulletA : MonoBehaviour
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

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("PlayerA"))
        {
            if(other.TryGetComponent(out PlayerControllerA playerControllerA))
            {
                playerControllerA.Die();
                Destroy(gameObject);
            }
        }
    }

    public void Shot (Vector3 dir, float speed)
    {
        moveSpeed = speed;
        bulletRigid.velocity = dir * moveSpeed;
    }
}
