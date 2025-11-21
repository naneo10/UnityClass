using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    [SerializeField] protected float moveSpeed = 3.0f;
    [SerializeField] protected int maxHp = 30;

    protected int currentHp;

    protected virtual void Start()
    {
        currentHp = maxHp;
    }

    public abstract void Attack();
    public virtual void TakeDamage(int damage)
    {
        currentHp -= damage;
        Debug.Log($"{gameObject.name}, {damage}, hp : {currentHp}");
    }

    protected virtual void Die()
    {
        Debug.Log($"{gameObject.name}이 죽었다");
        Destroy(gameObject);
    }

    public void Move(Vector3 direction)
    {
        transform.position += direction * moveSpeed * Time.deltaTime;
    }

    /*
    protected virtual void Die()
    {
        Debug.Log($"{gameObject.name}이 죽었다");
        Destroy(gameObject);
    }

    public class BossEnemy : EnemyBase
    {
        protected override void Die()
        {
            base.Die(); //로그 + 파괴
            + 보스의 추가적인 죽음을 알리고 싶다
        }
    }
    */
}
