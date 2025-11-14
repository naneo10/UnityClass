using System.Collections.Generic;
using UnityEngine;

public class BattleInventory : MonoBehaviour
{
    #region field
    private List<ItemDataSO> Items = InteractionManager.Instance.SaveItem;


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

    public void RemoveItem(ItemDataSO item)
    {
        if (item == null) return; //방어코드

        Items.Remove(item);
        FreshSlot();
    }
    #endregion
}