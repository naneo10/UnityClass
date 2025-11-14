using UnityEngine;

[CreateAssetMenu(fileName = "Item_", menuName = "Add Item/Item")]
public class ItemDataSO : ScriptableObject
{
    [Header("기본 정보")]
    public string itemName;
    public int damage;
    public int skillDamage;
    public int defense;
    public float speed;
    public int price;
    public Sprite itemimage;

    //소비 아이템
    [Header("소비 아이템")]
    public bool expendables;
    public float recoveryHp;
    public float recoveryMp;

    public int counter; //소지하고 있는 아이템 수
    public int sellCounter; //판매 묶음 갯수

    //장비 아이템
    [Header("장비 아이템")]
    public bool equipment;
    public bool helmet;
    public bool armor;
    public bool gloves;
    public bool pants;
    public bool shoes;
    public bool weapone;
}
