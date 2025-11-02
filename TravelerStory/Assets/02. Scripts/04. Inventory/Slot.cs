using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] Image image;
    [SerializeField] TextMeshProUGUI itemCounter;

    private ItemDataSO _item;
    public ItemDataSO Item
    {
        get { return _item; }
        set
        {
            _item = value;
            if (_item != null)
            {
                image.sprite = Item.itemimage;
                image.color = new Color(1, 1, 1, 1);

                if (_item.counter > 0)
                {
                    itemCounter.text = "" + _item.counter;
                }
            }
            else
            {
                image.color = new Color(1, 1, 1, 0);
            }
        }
    }

    private void Update()
    {
        if ( _item != null)
        {
            if (_item.counter > 0)
            {
                itemCounter.text = "" + _item.counter;
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            Debug.Log("Mouse Click Button : left");
        }

        if (InteractionManager.Instance != null)
        {
            InteractionManager.Instance.OnSlotClicked(this, eventData);
        }
    }
}
