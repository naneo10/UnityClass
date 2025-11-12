using UnityEngine;
using UnityEngine.UI;

public class MonsterBattle : MonoBehaviour
{
    #region field
    [SerializeField] public MonsterData monsterData;

    [Header("드랍 아이템")]
    [SerializeField] GameObject coin;
    #endregion

    #region mehod
    public void Die(MonsterData monster)
    {
        if (monster.hp <= 0)
        {
            Debug.Log("die 실행");
            //쿼터니언 : https://sohee1702.tistory.com/580
            Instantiate(coin, transform.position, Quaternion.Euler(0f, 0f, 240f));
        }
    }
    #endregion
}
