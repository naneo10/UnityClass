using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    #region field
    public List<MonsterData> data;

    [SerializeField] public GameObject[] monsters;
    //해당 인카운터에 맞춰 넘겨보낼 몬스터 데이터
    [SerializeField] public MonsterSlot[] monsterSlots;
    //범위 안에 들어가있는지 여부 확인
    [SerializeField] public MonsterEnCounter[] monsterEnCounter;
    #endregion

    void Awake()
    {
        FreshSlot();
    }

    #region method
    public void FreshSlot()
    {
        for (int i = 0; i < monsterSlots.Length; i++)
        {
            monsterSlots[i].MonsterData = data[i];
        }
    }

    public void TriggerCheck()
    {
        for (int i = 0; i < monsterEnCounter.Length; i++)
        {
            if (monsterEnCounter[i].monsterRangeIn)
            {
                InteractionManager.Instance.EnCounter(
                    monsterEnCounter[i].monsterRangeIn,
                    monsterSlots[i].MonsterData
                    );
            }
        }
    }
    #endregion
}
