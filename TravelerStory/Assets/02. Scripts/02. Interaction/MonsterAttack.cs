using UnityEngine;

public class MonsterAttack : MonoBehaviour
{
    public Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        
    }

    public void Attack()
    {
        anim.SetTrigger("Attack");
    }
}
