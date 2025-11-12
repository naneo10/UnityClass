using System.Net;
using UnityEngine;

public abstract class Skill
{
    #region field
    public string Name { get; protected set; }
    public int Damage { get; protected set; }
    public int Speed { get; protected set; }
    public int Mana { get; protected set; }

    public Skill (string name, int damage, int speed, int mana)
    {
        Name = name;
        Damage = damage;
        Speed = speed;
        Mana = mana;
    }
    #endregion

    #region method
    public abstract void UseSkill(PlayerStatus player, MonsterData monster);

    public abstract int SkillDamage(PlayerStatus player);
    #endregion
}

public class Fireball : Skill
{
    public static Fireball instance;

    public static Fireball Instance()
    {
        if (instance == null)
        {
            instance = new Fireball("파이어볼", 3, 0, 25);
        }
        return instance;
    }

    public Fireball (string name, int damage, int speed, int mana) : base (name, damage, speed, mana)
    {
        Name = name;
        Damage = damage;
        Speed = speed;
        Mana = mana;
    }

    public override void UseSkill(PlayerStatus player, MonsterData monster)
    {
        if (player.mp > 0 && player.mp - Mana >= 0)
        {
            int totalSkillDamage = player.skillDamage * Damage;
            monster.hp -= (float)totalSkillDamage;
            player.mp -= (float)Mana;
        }
        else
        {
            Debug.Log("마나 부족");
            return;
        }
    }

    public override int SkillDamage(PlayerStatus player)
    {
        int skillDamage = player.skillDamage * Damage;
        return skillDamage;
    }
}

public class IceSpear : Skill
{
    public static IceSpear instance;

    public static IceSpear Instance()
    {
        if (instance == null)
        {
            instance = new IceSpear("아이스 스피어", 2, 10, 20);
        }
        return instance;
    }

    public IceSpear (string name, int damage, int speed, int mana) : base (name, damage, speed, mana)
    {
        Name = name;
        Damage = damage;
        Speed = speed;
        Mana = mana;
    }

    public override void UseSkill(PlayerStatus player, MonsterData monster)
    {
        if (player.mp > 0 && player.mp - Mana >= 0)
        {
            int totalSkillDamage = player.skillDamage * Damage;
            monster.hp -= (float)totalSkillDamage;
            player.mp -= (float)Mana;
        }
        else
        {
            Debug.Log("마나 부족");
            return;
        }
    }

    public override int SkillDamage(PlayerStatus player)
    {
        int skillDamage = player.skillDamage * Damage;
        return skillDamage;
    }
}

public class DoubleAttack : Skill
{
    public static DoubleAttack instance;

    public static DoubleAttack Instance()
    {
        if (instance == null)
        {
            instance = new DoubleAttack("더블어택", 2, 0, 20);
        }
        return instance;
    }

    public DoubleAttack (string name, int damage, int speed, int mana) : base (name, damage, speed, mana)
    {
        Name = name;
        Damage = damage;
        Speed = speed;
        Mana = mana;
    }

    public override void UseSkill(PlayerStatus player, MonsterData monster)
    {
        if (player.mp > 0 && player.mp - Mana >= 0)
        {
            int totalSkillDamage = player.damage * Damage;
            monster.hp -= (float)totalSkillDamage;
            player.mp -= (float)Mana;
        }
        else
        {
            Debug.Log("마나 부족");
            return;
        }
    }

    public override int SkillDamage(PlayerStatus player)
    {
        int skillDamage = player.damage * Damage;
        return skillDamage;
    }
}


