using UnityEngine;

public class Monster : MonoBehaviour
{
    [SerializeField] private float speed;

    void Update()
    {
        transform.position += transform.forward * Time.deltaTime * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            if (KillManager.Instance != null)
            {
                KillManager.Instance.AddKill();
            }
            Destroy(gameObject);
        }
    }
}
