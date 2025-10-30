using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

///<summary>
///인벤토리 슬롯 하나를 관리하는 클래스입니다.
///자기 자신이 가지고 있는 데이터를 인벤토리 관리자에게 건네주고,
///또는 아이템 데이터를 받아옵니다.
///</summary>
public class InventorySlot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler,
    IPointerExitHandler
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
    [SerializeField] private Text mTextCount; //아이탬의 개수 텍스트

    [Header("아이템 쿨타임 이미지")]
    [SerializeField] private Image mCooltimeImage;
    #endregion

    private void Update()
    {
        //아이템 쿨타임 스프라이트를 쿨타임 기반으로 계산혀여 채웁니다.
        if (mItem != null) 
        { 
            mCooltimeImage.fillAmount =
                ItemCooltimeManager.Instance.GetCurrentCooltime(mItem.ItemID) / mItem.ItemCooltime;
        }
        else
        {
            mCooltimeImage.fillAmount = 0.0f;
        }
    }

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

    /// <summary>
    /// 마우스 클릭 오버라이드나 외부에서 해당 슬롯을 대상으로 직접 사용하도록 호출합니다.
    /// </summary>
    public void UseItem()
    {
        if (mItem != null) //해당 슬롯의 아이템이 null이라면 return
        {
            //상호착용이 불가능한 (사용이 불가능한) 아이템이라면 리턴
            if (!mItem.IsInteractivity) { return; }

            //쿨타임이 0보다 큰 경우 (현재 쿨타임이 돌고있는 경우)라면 리턴합니다.
            if (ItemCooltimeManager.Instance.GetCurrentCooltime(mItem.ItemID) > 0) { return; }

            //아이템 사용 함수 호출
            //만약 아이템 함수 호출인 상태에서 false가 리턴되면, 현재 사용 불가능 상태이기에 리턴합니다.
            if (!mItemActionManager.UseItem(mItem)) { return; }

            //아이템의 쿨타임이 설정되어있으면 쿨타임 적용
            if (mItem.ItemCooltime > 0f)
            {
                ItemCooltimeManager.Instance.AddCooltimeQueue(mItem.ItemID,
                mItem.ItemCooltime);
            }

            //상호작용이 가능한(착용 가능한) 장비 아이템을 사용한 경우?
            if (mItem.Type >= ItemType.Equipment_HELMET && mItem.Type <= ItemType.Equipment_SHOES)
            {
                ChangeEquipmentSlot();
            }
            
            //아이템이 소모성이면 한 개씩 개수를 줄입니다.
            if (mItem != null && mItem.IsConsumable) { UpdateSlotCount(-1); }

            //아이템을 다 쓴 경우, UpdateSlotCount로 인해 mItem이 null이 되는 경우에 UI를 끕니다.
            if (mItem == null) { mItemDescription.CloseUI(); }
        }
    }

    //클릭 이벤트
    //https://everenew.tistory.com/242
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right) //버튼 우클릭 시
        {
            if (mSlotMask == ItemType.SKILL) { return; }

            UseItem();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {

    }

    #endregion
}
