using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class InteractionManager : MonoBehaviour
{
    #region field
    public static InteractionManager Instance;
    public ItemDataSO itemDataSO;

    public Inventory cInventory;
    public EquipMent cEquipMent;

    [Header("인벤토리/스킬/장비")]
    [SerializeField] private GameObject inventory;
    [SerializeField] private GameObject Equipment;
    [SerializeField] private GameObject skill;

    //사용조건 충족 확인
    public bool useItem;

    //입력 키 값
    private bool inputI;
    private bool inputK;
    private bool inputP;
    #endregion

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
        inputP = Input.GetKeyDown(KeyCode.P);
        inputK = Input.GetKeyDown(KeyCode.K);

        if (inputI)
        {
            inventory.SetActive(!inventory.activeSelf);
        }

        if (inputP)
        {
            Equipment.SetActive(!Equipment.activeSelf);
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

    public void UseEquipment(Slot slot)
    {
        cEquipMent.AddEquipement(slot.Item);
    }

    public void UnEquipment(Slot slot)
    {
        cEquipMent.RemoveEquipment(slot.Item);
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
                    else if (slot.Item.equipment)
                    {
                        //Equirpment
                        UseEquipment(slot);
                    }
                }
                break;
            case PointerEventData.InputButton.Right:
                {
                    UnEquipment(slot);
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
