using UnityEngine;
using UnityEngine.UI;

public class Monster : MonoBehaviour
{
    #region field
    [Header("스폰 포인트")]
    [SerializeField] public MonsterBattle monsterBattle;
    [SerializeField] public Image monsterHpBar;

    [SerializeField] SpawnPoint BlueSpawnPoint;
    [SerializeField] SpawnPoint TurquoSpawnPoint;
    #endregion

    void Start()
    {
        SpawnMonster();
    }

    #region method
    private void AddMonster()
    {
        GameObject monsterObj = GameObject.FindGameObjectWithTag("Monster");
        GameObject monsterHpBar = GameObject.Find("MonsterHp");

        if (monsterObj != null) monsterBattle = monsterObj.GetComponent<MonsterBattle>();
        if (monsterHpBar != null) this.monsterHpBar = monsterHpBar.GetComponent<Image>();
    }

    public void SpawnMonster()
    {
        if (InteractionManager.Instance.LastMonster == null ||
            InteractionManager.Instance.LastMonster.Count == 0)
        {
            Debug.Log("MonstersInRange에 몬스터가 없음");
            return;
        }

        var monster = InteractionManager.Instance.LastMonster[0];

        if (monster.monsterData.monsterName == "FrankBlue")
        {
            if (BlueSpawnPoint != null)
            {
                GameObject blueMonster = BlueSpawnPoint.SpawnObject();
                AddMonster();
            }
        }
        else if (monster.monsterData.monsterName == "RattlesTurquo")
        {
            if (TurquoSpawnPoint != null)
            {
                GameObject turquoMonster = TurquoSpawnPoint.SpawnObject();
                AddMonster();
            }
        }
    }
    #endregion
}
