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
    public int recoveryHp;
    public int recoveryMp;

    public int counter;

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
