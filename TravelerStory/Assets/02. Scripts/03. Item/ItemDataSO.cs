using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Item_", menuName = "Add Item/Item")]
public class ItemDataSO : ScriptableObject
{
    public string itemName;
    public int damage;
    public float speed;
    public int price;
    public Sprite itemimage;

    public bool expendables;
    public int recoveryHp;
    public int recoveryMp;

    public int counter;
}
