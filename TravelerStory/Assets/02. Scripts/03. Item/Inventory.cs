using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    #region field
    //아이템을 담을 리스트
    public List<ItemDataSO> items;

    //Slot의 부모가 되는 Bag을 담을 곳
    [Header("ItemList 담을 곳")]
    [SerializeField] Transform slotParent;
    //ItemList 하위에 등록된 Slot을 담을 곳
    [SerializeField] private Slot[] slots;
    #endregion

    /*
    #if는 런타임 분기(if문) 이 아니라 컴파일 시점 분기입니다.
    즉, 조건에 따라 코드가 아예 포함되거나 제외됩니다.

    유니티 에디터에서 바로 작동하는 역할, 처음 인벤토리에 소스를 등록하면
    Console창에 에러 발생, ItemList를 넣어 주면 slots의 Slot들이 자동으로 등록
    */
#if UNITY_EDITOR
    private void OnValidate()
    {
        slots = slotParent.GetComponentsInChildren<Slot>();
    }
#endif

    void Awake()
    {
        //게임이 시작되면 items에 들어 있는 아이템을 인벤토리에 넣어 줌
        FreshSlot();
    }

    #region method
    public void FreshSlot()
    {
        int i = 0;
        //; 앞쪽이 비어 있음 → 이미 위쪽에서 i를 선언/초기화한 상태라는 뜻
        for (; i < items.Count && i < slots.Length; i++)
        {
            //slot.cs의 Item = Inventory.cs List<ItemDataSO> items[i]
            slots[i].Item = items[i];
        }
        for (; i < slots.Length; i++)
        {
            slots[i].Item = null;
        }
    }

    //slot.cs에서 _item으로 값을 담음
    public void AddItem(ItemDataSO _item)
    {
        if (items.Count < slots.Length)
        {
            items.Add(_item);
            FreshSlot();
        }
        else
        {
            print("슬롯이 가득 차 있습니다.");
        }
    }
    #endregion
}
