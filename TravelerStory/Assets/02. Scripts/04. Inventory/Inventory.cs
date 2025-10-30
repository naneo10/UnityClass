using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 인벤토리 베이스로써 인벤토리 슬롯들을 등록시키고 사용할 준비를 완료합니다.
/// 추상클래스로 작성하여 인벤토리 베이스 자체적으로 인스턴스 할 수 없게 합니다.
/// </summary>
abstract public class Inventory : MonoBehaviour
{
    private InventorySlot[] mSlots;

    //Inventory 최상위 부모(활성 / 비활성화 목적)
    [SerializeField] protected GameObject mInventoryBase;
    /*
    Slot들을 담을 부모 게임 오브젝트
    씬을 로드하면 하위에 있는 모든 자식들을 가져와 인벤토리를 초기화 합니다.
    */
    [SerializeField] protected GameObject mInventorySlotsParent;

    /// <summary>
    /// 인벤토리 베이스를 초기화 시켜줍니다
    /// </summary>
    protected void Awake()
    {
        if (mInventoryBase.activeSelf)
        {
            mInventoryBase.SetActive(false);
        }

        mSlots = mInventorySlotsParent.GetComponentsInChildren<InventorySlot>();
    }
}
