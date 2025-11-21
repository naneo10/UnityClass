using UnityEngine;

//IDamageble를 받았기 때문에 반드시 구현
public class Boss : MonoBehaviour, IDamageble
{
    [SerializeField] private int hp = 200;

    public void TakeDamage(int damage)
    {
        hp -= damage;

        Debug.Log($"보스가 {damage}만큼 피해를 입었다. hp : {hp}");

        if (hp <= 0)
        {
            Dead();
        }
    }

    private void Dead()
    {
        Destroy(gameObject);
    }
}
