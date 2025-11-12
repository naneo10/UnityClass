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

    [SerializeField] Transform skills;
    [SerializeField] private TextMeshProUGUI[] skillList;
    #endregion

#if UNITY_EDITOR
    private void OnValidate()
    {
        skillList = skills.GetComponentsInChildren<TextMeshProUGUI>();
    }
#endif

    private void Awake()
    {
        Start();
        FreshSkillDamage();
    }

    #region method
    public void FreshSkillDamage()
    {
        skillList[0].text = $"파이어볼 : {Fireball.Instance().SkillDamage(PlayerStatus.instance)}";
        skillList[1].text = $"아이스스피어 : {IceSpear.Instance().SkillDamage(PlayerStatus.instance)}";
        skillList[2].text = $"더블어택 : {DoubleAttack.Instance().SkillDamage(PlayerStatus.instance)}";
    }

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