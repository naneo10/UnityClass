using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class Button : MonoBehaviour, IPointerClickHandler
{
    #region field
    [SerializeField] TextMeshProUGUI text;
    #endregion

    void Awake()
    {
        if (text != null)
        {
            text.raycastTarget = false;
        }
    }

    #region method
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            Debug.Log("Mouse Click Button : left");
        }

        if (InteractionManager.Instance != null)
        {
            InteractionManager.Instance.OnButtonClicked(this, eventData);
        }
    }
    #endregion
}