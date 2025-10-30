using TMPro;
using UnityEngine;

/*
장비 인벤토리입니다.
InventoryMain과 같은 기능을 수행하지만, 목적이 다르기에 분리하였습니다.
장비 아이템 관리(장착, 장착해제, 효과 등)에 특화된 기능을 구현할 목적입니다.
*/
public class EquipmentInventory : Inventory
{
    public static bool IsInventoryActive = false; //인벤토리 활성화 되었는가?

    [Header("현재 계산된 수치를 표현할 텍스트 라벨들")]
    [SerializeField] private TextMeshProUGUI mDamageLabel;
    [SerializeField] private TextMeshProUGUI mDefenseLabel;

    new private void Awake()
    {
        base.Awake();
    }
    void Update()
    {
        //옵션이 켜져있는 경우 비활성화
        if (GameMenuManager.IsOptionActive) { return; }

        if (Input.GetKeyDown(keyManager.Instance.GetKeyCode("Equipment")))
        {
            if (mInventoryBase.activeInHierarchy)
            {
                mInventoryBase.SetActive(false);
                IsInventoryActive = false;

                UtilityManager.TrayLockCursor();
            }
            else
            {
                mInventoryBase.SetActive(true);
                IsInventoryActive = true;

                UtilityManager.UnlockCursor();
            }
        }
    }
}
