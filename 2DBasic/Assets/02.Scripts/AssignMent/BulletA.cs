using UnityEngine;

public class BulletA : MonoBehaviour
{
    private float bulletSpeed = 8.0f;
    private Rigidbody2D bulletRigid;

    void Awake()
    {
        bulletRigid = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        Destroy(gameObject, 2.0f);
    }

    public void Shot (Vector2 dir, float speed)
    {
        bulletSpeed = speed;
        bulletRigid.velocity = dir * bulletSpeed;
    }
}
