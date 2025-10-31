using UnityEngine;
using UnityEngine.EventSystems;

public class InteractionManager : MonoBehaviour
{
    #region field
    public static InteractionManager Instance;

    [Header("인벤토리/스킬/장비")]
    [SerializeField] private GameObject inventory;
    [SerializeField] private GameObject skill;
    [SerializeField] private GameObject Equipment;
    #endregion

    private bool inputI;
    private bool inputK;
    private bool inputP;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        Active();
    }

    #region method
    private void Active()
    {
        inputI = Input.GetKeyDown(KeyCode.I);
        inputK = Input.GetKeyDown(KeyCode.K);
        inputP = Input.GetKeyDown(KeyCode.P);

        if (inputI)
        {
            inventory.SetActive(!inventory.activeSelf);
        }
    }

    public void UseItem()
    {

    }

    public void OnSlotClicked(Slot slot, PointerEventData eventData)
    {
        switch (eventData.button)
        {
            case PointerEventData.InputButton.Left:
                {
                    Debug.Log($"Left-clicked item: {slot.Item.itemName}");
                }
                break;
            case PointerEventData.InputButton.Right:
                {
                    Debug.Log($"Right-clicked item: {slot.Item.itemName}");
                }
                break;
            default:
                {
                    Debug.Log("Other button clicked");
                }
                break;
        }
    }
    #endregion
}
