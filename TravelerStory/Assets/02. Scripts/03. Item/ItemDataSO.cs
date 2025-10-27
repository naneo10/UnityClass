using UnityEngine;
[CreateAssetMenu(fileName = "Item_", menuName = "SO/ItemData")]

public class ItemDataSO : ScriptableObject
{
    public int ID;
    public string itemName;
    public string type;
    public int damage;
    public int skillDamage;
    public int speed;
    public string description;
    public Sprite itemImage;
}
