using JetBrains.Annotations;
using UnityEngine;

[System.Flags]
public enum ItemType //아이템 유형
{
    ///<summary>
    ///NONE Type은 아이탬을 습즉하기위해 E키를 누른 경우, 인벤토리에 들어오지 않습니다.
    ///특별한 상호작용이 있는 오브젝트로 취급합니다.
    ///</summary>
    NONE                                = 0b0, //0
    SKILL                               = 0b1, //1

    //장비 아이템 영역
    //장비 아이템 타입에서 추가되는 경우, 증가하는 값으로 추가합니다.
    Equipment_HELMET                    = 0b10, //2
    Equipment_ARMORPLATE                = 0b100, //4
    Equipment_GLOVE                     = 0b1000, //8
    Equipment_PANTS                     = 0b10000, //16
    Equipment_SHOES                     = 0b100000, //32

    //장비 아이템이 아닌 아이템들(소모, 기타, 재료, 퀘스트 아이템 등)
    Etc                                 = 0b1000000, //64
    Consumalbe                          = 0b10000000, //128
    Ingredient                          = 0b100000000, //256
    Quest                               = 0b1000000000, //512
}

[CreateAssetMenu(fileName = "Item_", menuName = "SO/ItemData")]
public class ItemDataSO : ScriptableObject
{
    [Header("고유한 아이템의 ID(중복불가)")]
    [SerializeField] private int mItemID;

    public int ItemID //외부에서 읽을 수 있도록하는 창구
    {
        //값을 수정할 일이 없기에 set은 사용하지 않습니다
        get { return mItemID; } //실제 데이터를 저장하는 변수 private으로 외부에서 접근 불가
    }

    [Header("아이템의 중첩이 가능한가?")]
    [SerializeField] private bool mCanOverLap;

    public bool CanOverLap
    {
        get { return mCanOverLap; }
    }

    [Header("사용(상호작용)이 가능한 아이템인가?")]
    [SerializeField] private bool mIsInteractivity;

    public bool IsInteractivity
    {
        get { return mIsInteractivity; }
    }

    [Header("아이템을 사용하면 사라지는가?")]
    [SerializeField] private bool mIsConsumable;

    public bool IsConsumable
    {
        get { return mIsConsumable; }
    }

    [Header("아이템을 사용시 쿨타임")]
    [SerializeField] private float mItemCooltime = -1;

    public float ItemCooltime
    {
        get { return mItemCooltime; }
    }

    [Header("아이템의 타입")]
    [SerializeField] private ItemType mItemType;

    //.Flags로 설정하여 중복으로 선택 가능하도록 구현하였습니다.
    //특정한 아이템 슬롯에 넣을 수 있는지 마스크와 상호작용 또는 사용시 여러 조건을 확인하기 위해 사용합니다.
    public ItemType Type
    {
        get { return mItemType; }
    }

    [Header("인벤토리에서 보여질 아이템의 이미지")]
    [SerializeField] private Sprite mItemImage;

    public Sprite Image
    {
        get { return mItemImage; }
    }
}
