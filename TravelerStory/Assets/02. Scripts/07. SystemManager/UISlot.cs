using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class UISlot : MonoBehaviour, IPointerClickHandler
{
    #region field
    public GameManager cGameManager;

    [SerializeField] public int index;
    [SerializeField] public TextMeshProUGUI buttonName;
    [SerializeField] public TextMeshProUGUI skillName;
    #endregion

    private void Awake()
    {
        cGameManager = GameManager.Instance; //싱글톤 스크립트 연결 구문
        if (buttonName != null) buttonName.raycastTarget = false;
        if (skillName != null) skillName.raycastTarget = false;
    }

    #region method
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            cGameManager.OnSlotClicked(this, eventData);
        }
    }
    #endregion
}