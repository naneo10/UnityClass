using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private int value = 100;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        GameManager.Instance.AddScore(value);
        Destroy(gameObject);
    }
}
