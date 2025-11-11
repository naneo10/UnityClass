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
        Skill01,
        Skill02,
        Skill03,
        Back
    }

    public static GameManager Instance { get; private set; }

    public Transform Player;
    public PlayerStatus cPlayerStatus;
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

        cPlayerStatus = PlayerStatus.Instance();

        Debug.Log($"캐릭터 데미지 : {cPlayerStatus.damage}, 스킬 데미지 : {cPlayerStatus.skillDamage}");
    }

    void Start()
    {
        SetupScene();
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

        if (playerObj != null) Player = playerObj.GetComponent<Transform>();
        if (cPlayerBattle != null) this.cPlayerBattle = cPlayerBattle.GetComponent<PlayerBattle>();
        if (cMonster != null) this.cMonster = cMonster.GetComponent<Monster>();
        if (cUIManager != null) this.cUIManager = cUIManager.GetComponent<UIManager>();
    }

    public void SetupScene()
    {
        SpawnPlayer();
    }

    public void SpawnPlayer()
    {
        Player.position = PlayerSpawnPoint.position;
    }

    public void OnSlotClicked(UISlot slot, PointerEventData eventData)
    {
        switch (eventData.button)
        {
            case PointerEventData.InputButton.Left:
                {
                    SelectType select = (SelectType)slot.index;
                    switch (select)
                    {
                        case SelectType.Attack:
                            {

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

                            }
                            break;
                        case SelectType.Skill01:
                            {

                            }
                            break;
                        case SelectType.Skill02:
                            {

                            }
                            break;
                        case SelectType.Skill03:
                            {

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
    #endregion
}