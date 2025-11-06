using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IPointerClickHandler
{
    #region field
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
                //아이템 이미지 표기
                if (_item.itemimage != null)
                {
                    image.sprite = Item.itemimage;
                    image.color = new Color(1, 1, 1, 1);
                }
                else
                {
                    image.sprite = null;
                    image.color = new Color(1, 1, 1, 0);
                }

                //아이템 수량 카운트
                if (itemCounter != null)
                {
                    if (_item.counter > 0)
                    {
                        itemCounter.text = "" + _item.counter;
                    }
                    else
                    {
                        itemCounter.text = "";
                    }
                }
            }
            else
            {
                image.sprite = null;
                image.color = new Color(1, 1, 1, 0);
                if (itemCounter != null) itemCounter.text = "";
            }
        }
    }
    #endregion

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
            if (itemCounter == null) return;

            if (_item.counter > 0)
            {
                itemCounter.text = "" + _item.counter;
            }
        }
    }

    #region method
    public void OnPointerClick(PointerEventData eventData)
    {
        if (InteractionManager.Instance != null)
        {
            InteractionManager.Instance.OnSlotClicked(this, eventData);
        }
    }
    #endregion
}