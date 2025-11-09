using System.Collections.Generic;
using UnityEngine;

public class SkillList : MonoBehaviour
{
    #region field
    private List<Skill> skills = new List<Skill>();
    [SerializeField] private List<SkillSlot> slots;

    void Awake()
    {
        FreshSkill();
        FreshSkillSlot();
    }
    #endregion

    void Update()
    {

    }

    #region method
    public void FreshSkill()
    {
        skills.Add(Fireball.Instance());
        skills.Add(IceSpear.Instance());
        skills.Add(DoubleAttack.Instance());
    }

    public void FreshSkillSlot()
    {
        for (int i = 0; i < skills.Count; i++)
        {
            slots[i].skillName.text = "" + skills[i].Name;
            slots[i].skillDamage.text = "스킬 데미지 : " + skills[i].SkillDamage(PlayerStatus.instance);
            slots[i].skillDebuff.text = "디버프 수치 : " + skills[i].Speed;
        }
    }
    #endregion
}