using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    //리스트에 추가하는 식으로 아이템 획득
    public List<ItemDataSO> items;

    [SerializeField] Transform slotParent;
    [SerializeField] Slot[] slots;

#if UNITY_EDITOR
    private void OnValidate()
    {
        slots = slotParent.GetComponentsInChildren<Slot>();
    }
#endif

    void Awake()
    {
        FreshSlot();
    }

    public void FreshSlot()
    {
        int i = 0;
        for (; i < items.Count && i < slots.Length; i++)
        {
            slots[i].Item = items[i];
        }

        for (; i < slots.Length; i++)
        {
            slots[i].Item = null;
        }
    }

    public void AddItem(ItemDataSO item)
    {
        if (items.Count < slots.Length)
        {
            items.Add(item);
            FreshSlot();
        }
        else
        {
            print("슬롯이 가득 차 있습니다.");
        }
    }

    public void RemoveItem(ItemDataSO item)
    {
        if (item == null) return; //방어코드

        items.Remove(item);
        FreshSlot();
    }
}
