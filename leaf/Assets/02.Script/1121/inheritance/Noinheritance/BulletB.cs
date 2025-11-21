using UnityEngine;

public class BulletB : MonoBehaviour
{
    [SerializeField] private float speed = 10.0f;
    [SerializeField] private float lifeTime = 3.0f;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}
