using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour
{
    #region field
    [SerializeField] private GameObject battleUI;

    [SerializeField] private GameObject firstPage;

    [SerializeField] private GameObject skillPage;
    #endregion

    private void Awake()
    {
        
    }

    #region method
    public void OnSlotClicked(UISlot slot, PointerEventData eventData)
    {
        switch (eventData.button)
        {
            case PointerEventData.InputButton.Left:
                {
                    
                }
                break;
        }
    }
    #endregion
}