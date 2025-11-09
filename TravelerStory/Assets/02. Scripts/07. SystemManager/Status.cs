using TMPro;
using UnityEngine;

public class Status : MonoBehaviour
{
    #region field
    [SerializeField] private TextMeshProUGUI hpText;
    [SerializeField] private TextMeshProUGUI mpText;
    [SerializeField] private TextMeshProUGUI damageText;
    [SerializeField] private TextMeshProUGUI skillDamageText;
    [SerializeField] private TextMeshProUGUI defenseText;
    [SerializeField] private TextMeshProUGUI speedText;
    #endregion

    void Awake()
    {
        FreshStatus();
    }

    #region method
    public void FreshStatus()
    {
        hpText.text = "최대 체력 : " + PlayerStatus.Instance().MaxHp.ToString();
        mpText.text = "최대 마력 : " + PlayerStatus.Instance().MaxMp.ToString();
        damageText.text = "데미지 : " + PlayerStatus.Instance().damage.ToString();
        skillDamageText.text = "스킬 데미지 : " + PlayerStatus.Instance().skillDamage.ToString();
        defenseText.text = "방어력 : " + PlayerStatus.Instance().defense.ToString();
        speedText.text = "행동속도 : " + PlayerStatus.Instance().speed.ToString();
    }
    #endregion
}
