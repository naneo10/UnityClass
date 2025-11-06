using System.Collections.Generic;
using UnityEngine;

public class Store : MonoBehaviour
{
    #region field
    [Header("버튼 목록")]
    [SerializeField] public GameObject[] types;

    [Header("인벤토리 UI")]
    [SerializeField] public Inventory inventory;

    [Header("상점 항목별 리스트")]
    public List<ItemDataSO> itemList;
    public List<ItemDataSO> equipList;

    [Header("아이템 리스트")]
    public Transform itemListParents;
    public StoreSlot[] itemLists;

    [Header("장비 리스트")]
    public Transform equipListParents;
    public StoreSlot[] equipLists;

#if UNITY_EDITOR
    private void OnValidate()
    {
        itemLists = itemListParents.GetComponentsInChildren<StoreSlot>();
        equipLists = equipListParents.GetComponentsInChildren<StoreSlot>();
    }
#endif
    #endregion

    void Awake()
    {
        FreshList();
    }

    #region method
    public void FreshList()
    {
        if (itemList == null) return;

        //위에 조건의 for문에서 증감한 i 값을 가지고 아래 for문에서 활용
        //아래 for문이 위의 for문 내용을 덮어씌기 방지
        int i = 0;
        for (; i < itemList.Count && i < itemLists.Length; i++)
        {
            itemLists[i].Item = itemList[i];
        }

        for (; i < itemLists.Length; i++)
        {
            itemLists[i].Item = null;
        }

        if (i != 0)
        {
            i = 0;
            for (; i < equipList.Count && i < equipLists.Length; i++)
            {
                equipLists[i].Item = equipList[i];
            }

            for (; i < equipLists.Length; i++)
            {
                equipLists[i].Item = null;
            }
        }
    }

    public void Change()
    {
        types[0].SetActive(!types[0].activeSelf);
        types[1].SetActive(!types[1].activeSelf);
    }
    #endregion
}
