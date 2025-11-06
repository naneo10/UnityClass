using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StoreSlot : MonoBehaviour, IPointerClickHandler
{
    #region field
    [SerializeField] Image image;
    [SerializeField] TextMeshProUGUI itemPrice;

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
                    Debug.Log($"슬롯 아이템 이미지 : {_item.name}의 값이 null");
                    image.color = new Color(1, 1, 1, 0);
                }

                if (itemPrice != null)
                {
                    itemPrice.text = "" + _item.price;
                }
                else
                {
                    itemPrice.text = "";
                }
            }
            else
            {
                image.sprite = null;
                image.color = new Color(1, 1, 1, 0);
                if (itemPrice != null) itemPrice.text = "";
            }
        }
    }
    #endregion

    void Awake()
    {
        if (itemPrice != null)
        {
            itemPrice.raycastTarget = false;
        }
    }

    #region method
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            Debug.Log("Mouse Click Button : Left");
        }

        if (InteractionManager.Instance != null)
        {
            InteractionManager.Instance.OnStorelistClick(this, eventData);
        }
    }
    #endregion
}
