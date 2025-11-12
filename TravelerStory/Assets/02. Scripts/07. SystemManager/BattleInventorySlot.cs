using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BattleInventorySlot : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] Image image;
    [SerializeField] TextMeshProUGUI itemCounter;

    private ItemDataSO _itemData;
    public ItemDataSO ItemData
    {
        get { return _itemData; }
        set
        {
            _itemData = value;
            if (_itemData != null)
            {
                if (_itemData.itemimage != null)
                {
                    image.sprite = _itemData.itemimage;
                    image.color = new Color(1, 1, 1, 1);
                }
                else
                {
                    image.sprite = null;
                    image.color = new Color(1, 1, 1, 0);
                }

                if (itemCounter != null)
                {
                    itemCounter.text = "" + _itemData.counter;
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
        if (itemCounter != null) itemCounter.raycastTarget = false;
    }

    private void Update()
    {
        if (_itemData != null)
        {
            if (itemCounter == null) return;
            if (_itemData.counter > 0)
            {
                itemCounter.text = "" + _itemData.counter;
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            GameManager.Instance.OnItemClicked(this, eventData);
        }
    }
}