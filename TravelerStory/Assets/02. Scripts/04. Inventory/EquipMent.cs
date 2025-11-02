using System.Collections.Generic;
using UnityEngine;

public class EquipMent : MonoBehaviour
{
    public List<ItemDataSO> equipment;

    [SerializeField] Transform slotParent;

#if UNITY_EDITOR
    private void OnValidate()
    {
        
    }
#endif

    void Awake()
    {
        
    }
}
