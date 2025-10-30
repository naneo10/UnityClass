using System.ComponentModel.Design;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    #region field
    [Header("인벤토리/스킬/장비")]
    [SerializeField] private GameObject inventory;
    [SerializeField] private GameObject skill;
    [SerializeField] private GameObject Equipment;
    #endregion

    private bool inputI;
    private bool inputK;
    private bool inputP;

    void Update()
    {
        Active();
    }

    #region method
    private void Active()
    {
        inputI = Input.GetKeyDown(KeyCode.I);
        

        if (inputI)
        {
            inventory.SetActive(true);
        }
    }
    #endregion
}
