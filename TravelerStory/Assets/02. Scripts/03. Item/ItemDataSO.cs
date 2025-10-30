using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Item_", menuName = "Add Item/Item")]
public class ItemDataSO : ScriptableObject
{
    private string itemName;
    private int damage;
    private float speed;
    private int price;
    public Sprite itemimage;
}
