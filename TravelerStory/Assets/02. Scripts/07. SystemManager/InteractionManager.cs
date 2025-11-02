using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class InteractionManager : MonoBehaviour
{
    #region field
    public static InteractionManager Instance;
    public ItemDataSO itemDataSO;

    public Inventory cInventory;

    [Header("인벤토리/스킬/장비")]
    [SerializeField] private GameObject inventory;
    [SerializeField] private GameObject skill;
    [SerializeField] private GameObject Equipment;

    //사용조건 충족 확인
    public bool useItem;
    #endregion

    private bool inputI;
    private bool inputK;
    private bool inputP;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        Active();
    }

    #region method
    private void Active()
    {
        inputI = Input.GetKeyDown(KeyCode.I);
        inputK = Input.GetKeyDown(KeyCode.K);
        inputP = Input.GetKeyDown(KeyCode.P);

        if (inputI)
        {
            inventory.SetActive(!inventory.activeSelf);
        }
    }

    public void UseItem(Slot slot)
    {
        if (slot.Item.recoveryHp > 0 && slot.Item.recoveryMp == 0)
        {
            PlayerStatus.Instance().ModifyHP(slot.Item.recoveryHp);
            Debug.Log($"현재 HP:{PlayerStatus.Instance().hp}");

            //물약 사용 조건 미충족 시 카운트 갱신 방어
            if (useItem)
            {
                slot.Item.counter -= 1;
            }
                slot.Item.counter -= 1;

            if (slot.Item.counter <= 0)
            {
                cInventory.RemoveItem(slot.Item);
            }
        }
        else if (slot.Item.recoveryMp > 0 && slot.Item.recoveryHp == 0)
        {
            PlayerStatus.Instance().ModifyMP(slot.Item.recoveryMp);
            Debug.Log($"현제 MP:{PlayerStatus.Instance().mp}");

            if (useItem)
            {
                slot.Item.counter -= 1;
            }

            if (slot.Item.counter <= 0)
            {
                cInventory.RemoveItem(slot.Item);
            }
        }
    }

    public void UseEquipment()
    {

    }

    public void OnSlotClicked(Slot slot, PointerEventData eventData)
    {
        /*
        NullReferenceException 오류 발생 Inventory.FreshSlot의 빈칸 null 값 할당으로 인해
        생기는 문제로 아래 식으로 null 값일 경우 해당 메서드를 실행하지 않음으로 처리
        */
        if (slot.Item == null)
        {
            Debug.Log("empty slot click");
            return;
        }

        switch (eventData.button)
        {
            case PointerEventData.InputButton.Left:
                {
                    if (slot.Item.expendables)
                    {
                        //HP,MP Potion
                        UseItem(slot);
                    }
                    else if (!slot.Item.expendables)
                    {
                        //Equirpment
                        UseEquipment();
                    }
                }
                break;
            case PointerEventData.InputButton.Right:
                {
                    Debug.Log($"Right-clicked item: {slot.Item.itemName}");
                }
                break;
            default:
                {
                    Debug.Log("Other button clicked");
                }
                break;
        }
    }
    #endregion
}
