using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// 여러 아이템을 담을 가장 기본적인 인벤토리
/// </summary>
public class InventoryMain : Inventory
{
    public static bool IsInventoryActive = false; //인벤토리 활성화 되었는가?

    new void Awake()
    {
        //Awake를 사용하여 자기 자신도 초기화 하지만
        //부모 클래스인 Inventory도 초기화 하기 위해 base.Awake()를 사용합니다.
        base.Awake();
    }

    private void Update()
    {
        TryOpenInventory();
    }

    /// <summary>
    /// 인벤토리를 I키를 눌러 열거나 닫는다
    /// KeyBingding System을 추가로 구현하여 키 설정을 통해 인벤토리를 활성화합니다
    /// </summary>
    private void TryOpenInventory()
    {
        //옵션이 켜져있는 경우 비활성화
        if (GameMenuManager.IsOptionActive) { return; }

        if (Input.GetKeyDown(KeyManager.Instance.GetKeyCode("Inventory")))
        {
            if (!IsInventoryActive)
                OpenInventory();
            else
                CloseInventory();
        }
    }

    /// <summary>
    /// 인벤토리를 엽니다
    /// </summary>
    private void OpenInventory()
    {
        mInventoryBase.SetActive(true);
        IsInventoryActive = true;

        //커서 활성화
        UtilityManager.UnlockCursor();
    }

    /// <summary>
    /// 인벤토리를 닫습니다.
    /// </summary>
    private void CloseInventory()
    {
        mInventoryBase.SetActive(false);
        IsInventoryActive = false;

        //커서 비활성화
        UtilityManager.TryLockCursor();
    }
}
