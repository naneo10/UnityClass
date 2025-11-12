using UnityEngine;
using UnityEngine.UI;

public class MonsterBattle : MonoBehaviour
{
    #region field
    [Header("무기")]
    [SerializeField] private GameObject weapone;

    [SerializeField] public MonsterData monsterData;
    [SerializeField] public MonsterAttack monsterAttack;

    private Animator anim;

    [Header("드랍 아이템")]
    [SerializeField] GameObject coin;
    #endregion

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    #region mehod
    public void Attack()
    {

    }

    public void DoubleAttack()
    {

    }

    public void Hit()
    {
        //setTrigger : https://sam0308.tistory.com/67
        anim.SetTrigger("Hit");
    }

    public void Die(MonsterData monster)
    {
        if (monster.hp <= 0)
        {
            anim.SetTrigger("Die");
            weapone.SetActive(false);
            //쿼터니언 : https://sohee1702.tistory.com/580
            Instantiate(coin, transform.position, Quaternion.Euler(0f, 0f, 240f));
        }
    }
    #endregion
}
