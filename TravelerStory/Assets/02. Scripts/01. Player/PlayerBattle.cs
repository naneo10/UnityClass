using UnityEngine;
using UnityEngine.SceneManagement;
using static GameManager;

public class PlayerBattle : MonoBehaviour
{
    #region field
    private Player player;
    public BattleInventory cBattleInventory;

    //아이템 사용조건 확인
    public bool useItem;
    #endregion

    void Awake()
    {
        player = Player.Instance; //애니메이션
    }

    #region method
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, LoadSceneMode mode)
    {
        GameObject cBattleInventory = GameObject.Find("ItemPage");
        if (cBattleInventory != null) this.cBattleInventory = cBattleInventory.GetComponent<BattleInventory>();
    }

    public void Attack(PlayerStatus player, MonsterData monster)
    {
        monster.hp -= (float)player.damage;
    }

    public void UseSkill(SelectType select, PlayerStatus player, MonsterData monster)
    {
        if (select == SelectType.Fireball)
        {
            Fireball.Instance().UseSkill(player, monster);
        }
        else if (select == SelectType.IceSpear)
        {
            IceSpear.Instance().UseSkill(player, monster);
        }
        else if (select == SelectType.DoubleAttack)
        {
            DoubleAttack.Instance().UseSkill(player, monster);
        }
    }

    public void UseItem(BattleInventorySlot slot)
    {
        if (slot.ItemData.recoveryHp > 0 && slot.ItemData.recoveryMp == 0)
        {
            PlayerStatus.Instance().ModifyHP(slot.ItemData.recoveryHp);
            Debug.Log($"현재 HP:{PlayerStatus.Instance().hp}");

            //물약 사용 조건 미충족 시 카운트 갱신 방어
            if (useItem)
            {
                slot.ItemData.counter -= 1;
            }

            if (slot.ItemData.counter <= 0)
            {
                cBattleInventory.RemoveItem(slot.ItemData);
            }
        }
        else if (slot.ItemData.recoveryMp > 0 && slot.ItemData.recoveryHp == 0)
        {
            PlayerStatus.Instance().ModifyMP(slot.ItemData.recoveryMp);
            Debug.Log($"현제 MP:{PlayerStatus.Instance().mp}");

            if (useItem)
            {
                slot.ItemData.counter -= 1;
            }

            if (slot.ItemData.counter <= 0)
            {
                cBattleInventory.RemoveItem(slot.ItemData);
            }
        }
    }

    public void Win()
    {

    }
    #endregion
}
