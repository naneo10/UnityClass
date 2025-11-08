using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillList : MonoBehaviour
{
    #region field
    public static SkillList Instance { get; private set; }

    private List<Skill> skills = new List<Skill>();
    [SerializeField] private List<SkillSlot> slots;

    void Awake()
    {
        //Scean1 scean2 둘 다 사용
        if (Instance != null && Instance != this)
        {
            Destroy(Instance);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        FreshSkill();
        FreshSlot();
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

    public void FreshSlot()
    {
        for (int i = 0; i < skills.Count; i++)
        {
            slots[i].skillName.text = "" + skills[i].Name;
            slots[i].skillDamage.text = "스킬 데미지 : " + skills[i].Damage;
            slots[i].skillDebuff.text = "디버프 수치 : " + skills[i].Speed;
        }
    }
    #endregion
}
