using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class UISlot : MonoBehaviour, IPointerClickHandler
{
    #region field
    public UIManager cUIManager;

    [SerializeField] public TextMeshProUGUI buttonName;
    #endregion

    private void Awake()
    {
        cUIManager = GetComponent<UIManager>();
        if (buttonName != null) buttonName.raycastTarget = false;
    }

    #region method
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            cUIManager.OnSlotClicked(this, eventData);
        }
    }
    #endregion
}