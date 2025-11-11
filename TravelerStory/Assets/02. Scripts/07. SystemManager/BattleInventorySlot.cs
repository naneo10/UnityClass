using UnityEngine;
using UnityEngine.UI;

public class BattleInventorySlot : MonoBehaviour
{
    #region field
    [SerializeField] Image image;
    private ItemDataSO _itemData;

    public ItemDataSO ItemData
    {
        get { return _itemData; }
        set
        {
            _itemData = value;
            if (_itemData != null)
            {

            }
        }
    }
    #endregion

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    #region method
    #endregion
}