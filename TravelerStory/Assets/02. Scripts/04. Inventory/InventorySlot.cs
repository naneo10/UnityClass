using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

///<summary>
///인벤토리 슬롯 하나를 관리하는 클래스입니다.
///자기 자신이 가지고 있는 데이터를 인벤토리 관리자에게 건네주고,
///또는 아이템 데이터를 받아옵니다.
///</summary>
public class InventorySlot : MonoBehaviour
{
    #region field
    private ItemDataSO mItem;
    public ItemDataSO Item
    {
        get { return mItem; }
    }

    [Header("해당 슬롯에 어떠한 타입만 들어올 수 있는지 타입 마스크")]
    [SerializeField] private ItemType mSlotMask;

    private int mItemCount; //획득한 아이탬의 개수

    [Header("아이템 슬롯에 있는 UI 오브젝트")]
    [SerializeField] private Image mItemImage; //아이탬의 이미지
    [SerializeField] private Image mCooltimeImage; //아이템 쿨타임 이미지
    [SerializeField] private Text mTextCount; //아이탬의 개수 텍스트
    #endregion

    #region method
    //아이탬 이미지의 투명도 조절
    private void SetColor(float _alpha)
    {
        Color color = mItemImage.color;
        color.a = _alpha;
        mItemImage.color = color;
    }

    /// <summary>
    /// mSlotMask에서 설정된 값에 따라 비트연산을 합니다.
    /// 현재 마스크값이 비트연산으로 0이 나온다면 현재 슬롯에 마스크가 일치하지 않는다는 뜻
    /// 0이 아닌 수는 현재 비트위치(10진수로 1, 2, 4, 8)로 값이 나옵니다.
    /// </summary>
    public bool IsMask(ItemDataSO item)
    {
        return ((int)item.Type & (int)mSlotMask) == 0 ? false : true;
    }

    //인벤토리에 새로운 아이템 슬롯 추가
    public void AddItem(ItemDataSO item, int count = 1)
    {
        mItem = item;
        mItemCount = count;
        mItemImage.sprite = mItem.Image;

        if (mItem.Type <= ItemType.Equipment_SHOES)
        {
            mTextCount.text = "";
        }
        else
        {
            mTextCount.text = mItemCount.ToString();
        }

        SetColor(1);
    }

    //해당 슬롯의 아이템 개수 업데이트
    public void UpdateSlotCount(int count)
    {
        mItemCount += count;
        mTextCount.text = mItemCount.ToString();

        if (mItemCount <= 0 )
        {
            ClearSlot();
        }
    }

    //해당 슬롯 하나 삭제합니다.
    public void ClearSlot()
    {
        mItem = null;
        mItemCount = 0;
        mItemImage.sprite = null;
        SetColor(0);

        mTextCount.text = "";
    }
    #endregion
}
