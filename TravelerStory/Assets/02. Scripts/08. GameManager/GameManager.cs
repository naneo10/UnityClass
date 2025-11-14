using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region field
    public enum SelectType
    {
        Attack,
        Skill,
        Item,
        ItemBack,
        Fireball,
        IceSpear,
        DoubleAttack,
        Back
    }

    public static GameManager Instance { get; private set; }

    public Transform PlayerObj;
    public PlayerBattle cPlayerBattle;
    public Monster cMonster;
    public UIManager cUIManager;

    [Header("스폰 포인트")]
    [SerializeField] Transform PlayerSpawnPoint;
    #endregion

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(Instance);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        //씬 전환 후 사용되지 않는 기능 잠금 I, K, N, P 등
        InteractionManager.Instance.changeScene = true;
        if (!InteractionManager.Instance.changeScene) return;

        Debug.Log($"캐릭터 데미지 : {PlayerStatus.instance.damage}, 스킬 데미지 : {PlayerStatus.instance.skillDamage}");
    }

    void Start()
    {
        SpawnPlayer();
    }

    #region method
    public void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, LoadSceneMode mode)
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        GameObject cPlayerBattle = GameObject.Find("PlayerBattle");
        GameObject cMonster = GameObject.Find("Monster");
        GameObject cUIManager = GameObject.Find("UIManager");

        if (playerObj != null) PlayerObj = playerObj.GetComponent<Transform>();
        if (cPlayerBattle != null) this.cPlayerBattle = cPlayerBattle.GetComponent<PlayerBattle>();
        if (cMonster != null) this.cMonster = cMonster.GetComponent<Monster>();
        if (cUIManager != null) this.cUIManager = cUIManager.GetComponent<UIManager>();
    }

    public void SpawnPlayer()
    {
        PlayerObj.position = PlayerSpawnPoint.position;
    }

    public void OnSlotClicked(UISlot slot, PointerEventData eventData)
    {
        MonsterData monster = cMonster.monsterBattle.monsterData;
        switch (eventData.button)
        {
            case PointerEventData.InputButton.Left:
                {
                    SelectType select = (SelectType)slot.index;
                    switch (select)
                    {
                        case SelectType.Attack:
                            {
                                cPlayerBattle.Attack(PlayerStatus.instance, monster);
                                cMonster.CurrentStatus(monster);
                                cMonster.monsterBattle.Hit();
                                cMonster.monsterBattle.Die(monster);
                                cPlayerBattle.Win(
                                    monster,
                                    InteractionManager.Instance,
                                    cPlayerBattle.cBattleInventory);
                            }
                            break;
                        case SelectType.Skill:
                            {
                                cUIManager.SelectSkill();
                            }
                            break;
                        case SelectType.Item:
                            {
                                cUIManager.SelectItem();
                            }
                            break;
                        case SelectType.ItemBack:
                            {
                                cUIManager.CloseItem();
                            }
                            break;
                        case SelectType.Fireball:
                            {
                                cPlayerBattle.UseSkill(select, PlayerStatus.instance, monster);
                                cMonster.CurrentStatus(monster); //몬스터 HP
                                Player.Instance.ChangeBarAmount(); //플레이어 HP,MP
                                Player.Instance.CurrentStatusText();
                                cMonster.monsterBattle.Hit();
                                cMonster.monsterBattle.Die(monster);
                                cPlayerBattle.Win(
                                    monster,
                                    InteractionManager.Instance,
                                    cPlayerBattle.cBattleInventory);
                            }
                            break;
                        case SelectType.IceSpear:
                            {
                                cPlayerBattle.UseSkill(select, PlayerStatus.instance, monster);
                                cMonster.CurrentStatus(monster);
                                Player.Instance.ChangeBarAmount();
                                Player.Instance.CurrentStatusText();
                                cMonster.monsterBattle.Hit();
                                cMonster.monsterBattle.Die(monster);
                                cPlayerBattle.Win(
                                    monster,
                                    InteractionManager.Instance,
                                    cPlayerBattle.cBattleInventory);
                            }
                            break;
                        case SelectType.DoubleAttack:
                            {
                                cPlayerBattle.UseSkill(select, PlayerStatus.instance, monster);
                                cMonster.CurrentStatus(monster);
                                Player.Instance.ChangeBarAmount();
                                Player.Instance.CurrentStatusText();
                                cMonster.monsterBattle.Hit();
                                cMonster.monsterBattle.Die(monster);
                                cPlayerBattle.Win(
                                    monster,
                                    InteractionManager.Instance,
                                    cPlayerBattle.cBattleInventory);
                            }
                            break;
                        case SelectType.Back:
                            {
                                cUIManager.CloseSkill();
                            }
                            break;
                    }
                }
                break;
        }
    }

    public void OnItemClicked(BattleInventorySlot slot, PointerEventData eventData)
    {
        if (slot.ItemData == null)
        {
            Debug.Log("빈 슬롯 클릭");
            return;
        }

        switch (eventData.button)
        {
            case PointerEventData.InputButton.Left:
                {
                    cPlayerBattle.UseItem(slot);
                    Player.Instance.ChangeBarAmount();
                    Player.Instance.CurrentStatusText();
                }
                break;
        }
    }
    #endregion
}