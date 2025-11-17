using UnityEngine;

public class BulletMove : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10.0f;
    [SerializeField] private float lifeTime = 3.0f;

    private float timer = 0.0f;

    void Start()
    {
        Destroy(gameObject, lifeTime); //#1 예시
    }

    void Update()
    {
        //#2 예시
        //timer += Time.deltaTime;
        //if (timer > 3.0f)
        //{
        //    Destroy(gameObject);
        //}

        transform.position += transform.forward * moveSpeed * Time.deltaTime;
    }
}
