using UnityEngine;

public class CEnemy : MonoBehaviour, IDamageble
{
    [SerializeField] private int maxHp = 20;
    private int currentHp;

    void Start()
    {
        currentHp = maxHp;
    }

    public void TakeDamage(int damage)
    {
        currentHp -= damage;
        Debug.Log($"Àâ¸÷ÀÌ ¸Â¾Ò´Ù!!! hp : {currentHp}/{maxHp}");

        if (currentHp <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Àâ¸÷ÀÌ Á×¾ú´Ù");
        Destroy(gameObject);
    }
}
