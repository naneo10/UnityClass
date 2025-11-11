using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour
{
    #region field
    [SerializeField] private GameObject battleUI;
    [SerializeField] private GameObject firstPage;
    [SerializeField] private GameObject skillPage;
    [SerializeField] private GameObject itemPage;
    #endregion

    private void Awake()
    {
        Start();
    }

    #region method
    private void Start()
    {
        battleUI.SetActive(true);
        firstPage.SetActive(true);
    }

    public void SelectSkill()
    {
        firstPage.SetActive(false);
        skillPage.SetActive(true);
    }

    public void CloseSkill()
    {
        skillPage.SetActive(false);
        firstPage.SetActive(true);
    }

    public void SelectItem()
    {
        firstPage.SetActive(false);
        itemPage.SetActive(true);
    }

    public void CloseItem()
    {
        itemPage.SetActive(false);
        firstPage.SetActive(true);
    }

    public void End()
    {
        battleUI.SetActive(false);
    }
    #endregion
}