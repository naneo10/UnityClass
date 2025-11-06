using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class InteractionManager : MonoBehaviour
{
    #region field
    public static InteractionManager Instance;
    public ItemDataSO itemDataSO;

    [Header("클래스 인스턴스")]
    public Inventory cInventory;
    public EquipMent cEquipMent;
    public Store cStore;
    public Gold cGold;

    [Header("인벤토리/스킬/장비")]
    [SerializeField] private GameObject inventory;
    [SerializeField] private GameObject Equipment;
    [SerializeField] private GameObject skill;

    [Header("상점")]
    [SerializeField] public GameObject store;
    private bool rangeIn;

    //사용조건 충족 확인
    public bool useItem;
    public bool sameItem;
    public bool useEquip = false;

    //입력 키 값
    private bool inputI;
    private bool inputK;
    private bool inputP;

    //상호작용 키 값
    private bool inputF;
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
        inputF = Input.GetKeyDown(KeyCode.F);

        if (inputI)
        {
            inventory.SetActive(!inventory.activeSelf);
        }

        if (inputP)
        {
            Equipment.SetActive(!Equipment.activeSelf);
        }

        if (inputF && rangeIn)
        {
            store.SetActive(!store.activeSelf);
        }
    }

    public void IsNear(bool check)
    {
        rangeIn = check;
    }

    private void UseItem(Slot slot)
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

    private void UseEquipment(Slot slot)
    {
        cEquipMent.AddEquipement(slot.Item);
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
                        UseEquipment(slot);
                    }
                }
                break;
            default:
                {
                    Debug.Log("Other button clicked");
                }
                break;
        }
    }

    private void UnEquipment(EquipSlot equipSlot)
    {
        cEquipMent.RemoveEquipment(equipSlot.Item);
    }

    public void OnEquipmentClicked(EquipSlot equipSlot, PointerEventData eventData)
    {
        switch(eventData.button)
        {
            case PointerEventData.InputButton.Right:
                {
                    UnEquipment(equipSlot);
                }
                break;
            default:
                {
                    Debug.Log("Other button clicked");
                }
                break;
        }
    }

    public void ChangeSlot()
    {
        cStore.Change();
    }

    public void OnButtonClicked(Button button, PointerEventData eventData)
    {
        switch (eventData.button)
        {
            case PointerEventData.InputButton.Left:
                {
                    ChangeSlot();
                }
                break;
            default:
                {
                    Debug.Log("Other button clicked");
                }
                break;
        }
    }

    private void BuyItem(StoreSlot storeSlot)
    {
        if (PlayerStatus.Instance().Gold >= storeSlot.Item.price)
        {
            cGold.SubtractGold(storeSlot.Item.price);
            cInventory.AddItem(storeSlot.Item);
        }
        else if (PlayerStatus.Instance().Gold < storeSlot.Item.price)
        {
            Debug.Log("골드가 부족합니다");
            return;
        }
    }

    public void OnStorelistClick(StoreSlot storeSlot, PointerEventData eventData)
    {
        if (storeSlot.Item == null)
        {
            Debug.Log("empty slot click");
            return;
        }

        switch (eventData.button)
        {
            case PointerEventData.InputButton.Left:
                {
                    BuyItem(storeSlot);
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
