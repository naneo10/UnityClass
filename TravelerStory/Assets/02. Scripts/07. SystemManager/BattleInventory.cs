using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BattleInventory : MonoBehaviour
{
    #region field
    private List<ItemDataSO> Items;

    [SerializeField] private Transform ParentSlot;
    [SerializeField] private BattleInventorySlot[] slot;
    #endregion

#if UNITY_EDITOR
    private void OnValidate()
    {
        slot = ParentSlot.GetComponentsInChildren<BattleInventorySlot>();
    }
#endif

    void Awake()
    {
        FreshSlot();
    }

    #region method
    public void FreshSlot()
    {
        int i = 0;
        for (; i < Items.Count; i++)
        {
            slot[i].ItemData = Items[i];
        }

        for (; i < slot.Length; i++)
        {
            slot[i].ItemData = null;
        }
    }

    public void AddItem(ItemDataSO item)
    {
        if (Items.Count < slot.Length)
        {
            Items.Add(item);
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

        Items.Remove(item);
        FreshSlot();
    }
    #endregion
}