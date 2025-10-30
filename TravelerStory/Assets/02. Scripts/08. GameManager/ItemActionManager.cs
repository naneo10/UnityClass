using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// 씬 내의 매니저 오브젝트에 할당합니다.
/// 아이탬(또는 정적 물체)과 상호작용하거나, 인벤토리에서 아이템을 사용하면 특수 이벤트를 발생시킵니다.
/// </summary>
public class ItemActionManager : MonoBehaviour
{
    [Header("장면에 미리 로드된 객체")]
    [SerializeField] private GameObject[] mObjects;

    public bool UseItem(ItemDataSO item)
    {
        Debug.Log("UseItemEvent");

        switch (item.Type)
        {
            case ItemType.Consumable:
            {
                switch (item.ItemID)
                {
                    case (int)ItemCode.SMALL_HEALTH_POTION:
                    {
                        GameManager.Instance.playerStatus.ModifyHP(50);
                        break;
                    }
                    case (int)ItemCode.SMALL_MANA_POTION:
                    {
                        GameManager.Instance.playerStatus.ModifyMP(50);
                        break;
                    }
                }
                break;
            }
        }
        return true;
    }

    public enum ItemCode
    {
        SMALL_HEALTH_POTION,
        SMALL_MANA_POTION,
    }
}
