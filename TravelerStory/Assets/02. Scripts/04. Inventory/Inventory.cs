using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;

    #region field
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

    #endregion
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

            FreshSlot();
    }

    #region method
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
        ItemDataSO duplication = null;

        if (item.recoveryHp > 0 && item.recoveryMp == 0)
        {
            duplication = items.Find(x => x.recoveryHp > 0);
        }
        else if (item.recoveryMp > 0 && item.recoveryHp == 0)
        {
            duplication = items.Find(x => x.recoveryMp > 0);
        }

        if (items.Count >= slots.Length)
        {
            print("슬롯이 가득 차 있습니다.");
            return;
        }

        if (duplication != null)
        {
            duplication.counter++;
        }
        else
        {
            items.Add(item);
        }

        FreshSlot();
    }

    public void RemoveItem(ItemDataSO item)
    {
        if (item == null) return; //방어코드

        items.Remove(item);
        FreshSlot();
    }

    public void LoadItem(InteractionManager interactionManager)
    {
        items = interactionManager.SaveItem;
    }
    #endregion
}
