using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using static GameManager;

public class PlayerBattle : MonoBehaviour
{
    #region field
    private Player playerObj;
    private PlayerStatus playerStatus;
    public BattleInventory cBattleInventory;

    //아이템 사용조건 확인
    public bool useItem;
    #endregion

    void Awake()
    {
        playerObj = Player.Instance; //애니메이션
        playerStatus = PlayerStatus.Instance();
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
        playerObj.AttackMotion();
    }

    public void UseSkill(SelectType select, PlayerStatus player, MonsterData monster)
    {
        if (select == SelectType.Fireball)
        {
            Fireball.Instance().UseSkill(player, monster);
            playerObj.skillEffect.FireballEffect();
        }
        else if (select == SelectType.IceSpear)
        {
            IceSpear.Instance().UseSkill(player, monster);
            playerObj.skillEffect.IceSpearEffect();
        }
        else if (select == SelectType.DoubleAttack)
        {
            DoubleAttack.Instance().UseSkill(player, monster);
            playerObj.DoubleAttackMotion();
        }
    }

    public void UseItem(BattleInventorySlot slot)
    {
        if (slot.ItemData.recoveryHp > 0 && slot.ItemData.recoveryMp == 0)
        {
            playerStatus.ModifyHP(slot.ItemData.recoveryHp);
            Debug.Log($"현재 HP:{playerStatus.hp}");

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
            playerStatus.ModifyMP(slot.ItemData.recoveryMp);
            Debug.Log($"현제 MP:{playerStatus.mp}");

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

    public void Win(MonsterData monster)
    {
        if (monster.hp <= 0)
        {
            StartCoroutine(EndEnCounter(3.0f));
        }
    }

    private IEnumerator EndEnCounter(float delay)
    {
        yield return new WaitForSeconds(delay);
        Debug.Log("전투 승리");
        SceneManager.LoadScene("01.Village");
        InteractionManager.Instance.changeScene = false; //잠긴 기능 해제
    }
    #endregion
}
