using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EquipSlot : MonoBehaviour, IPointerClickHandler
{
    #region field
    [SerializeField] public Image image;
    [SerializeField] public TextMeshProUGUI equipName;

    private ItemDataSO _item;

    public ItemDataSO Item
    {
        get { return _item; }
        set
        {
            _item = value;
            if (_item != null)
            {
                if (_item.itemimage != null)
                {
                    image.sprite = _item.itemimage;
                    image.color = new Color(1, 1, 1, 1);
                }
                else
                {
                    image.sprite = null;
                    image.color = new Color(1, 1, 1, 0);
                }

                if (equipName != null)
                {
                    equipName.text = "" + _item.itemName;
                }
                else
                {
                    equipName.text = "";
                }
            }
            else
            {
                image.sprite = null;
                image.color = new Color(1, 1, 1, 0);
                if (equipName != null) equipName.text = "";
            }
        }
    }
    #endregion

    void Awake()
    {
        if (equipName != null)
        {
            equipName.raycastTarget = false;
        }
    }

    #region method
    public void OnPointerClick(PointerEventData eventData)
    {
        if (InteractionManager.Instance != null)
        {
            InteractionManager.Instance.OnEquipmentClicked(this, eventData);
        }
    }
    #endregion
}
