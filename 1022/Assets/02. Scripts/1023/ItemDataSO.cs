using UnityEngine;
[CreateAssetMenu(fileName = "Item_", menuName = "Game/ItemData")]
public class ItemDataSO : ScriptableObject
{
    public int ID;
    public string itemName;
    public string type;
    public int power;
    public string description;
}