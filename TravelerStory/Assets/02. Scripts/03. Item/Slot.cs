using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    //image Compnent를 담을 곳
    [SerializeField] Image Image;

    private ItemDataSO _item;

    public ItemDataSO Item
    {
        get { return _item; }
        set
        {
            _item = value;
            if (_item != null)
            {
                Image.sprite = Item.itemImage;
                Image.color = new Color(1, 1, 1, 1);
            }
            else
            {
                Image.color = new Color(1, 1, 1, 0);
            }
        }
    }
}
