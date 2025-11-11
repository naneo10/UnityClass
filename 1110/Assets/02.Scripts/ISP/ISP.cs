using UnityEngine;

//움직일 수 있는 능력만 약속하는 인터페이스
public interface IMoveable
{
    public float MoveSpeed { get; set; }
    public float GoForward();
}

public interface IDamageable
{
    public float Health { get; set; }
    public void Die();
    public void TakeDamage();
}

public class ISP : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
