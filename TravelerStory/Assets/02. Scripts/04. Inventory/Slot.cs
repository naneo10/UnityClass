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
                if (_item.itemimage != null)
                {
                    image.sprite = Item.itemimage;
                    image.color = new Color(1, 1, 1, 1);
                }
                else
                {
                    Debug.Log($"슬롯 아이템 이미지 : {_item.name}의 값이 null");
                    image.color = new Color(1, 1, 1, 0);
                }

                if (_item.counter > 0)
                {
                    itemCounter.text = "" + _item.counter;
                }
                else
                {
                    itemCounter.text = "";
                }
            }
            else
            {
                image.sprite = null;
                image.color = new Color(1, 1, 1, 0);
                itemCounter.text = "";
            }
        }
    }

    private void Awake()
    {
        //텍스트가 클릭 범위를 막지 않게 비활성화
        if (itemCounter != null)
        {
            itemCounter.raycastTarget = false;
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

        if (eventData.button == PointerEventData.InputButton.Right)
        {
            Debug.Log("Mouse Click Button : Right");
        }

        if (InteractionManager.Instance != null)
        {
            InteractionManager.Instance.OnSlotClicked(this, eventData);
        }
    }
}