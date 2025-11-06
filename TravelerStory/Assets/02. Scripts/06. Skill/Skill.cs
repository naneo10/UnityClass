using UnityEngine;

public abstract class Skill
{
    #region field
    public string Name { get; protected set; }
    public int Damage { get; protected set; }
    public int Speed { get; protected set; }

    public Skill (string name, int damage, int speed)
    {
        Name = name;
        Damage = damage;
        Speed = speed;
    }
    #endregion

    #region method
    public abstract void UseSkill(PlayerStatus player, MonsterData monster);
    #endregion
}

public class FireBall : Skill
{
    public static FireBall instance;

    public static FireBall Instance()
    {
        if (instance == null)
        {
            instance = new FireBall("파이어볼", 30, 0);
        }
        return instance;
    }

    public FireBall (string name, int damage, int speed) : base (name, damage, speed)
    {
        Name = name;
        Damage = damage;
        Speed = speed;
    }

    public override void UseSkill(PlayerStatus player, MonsterData monster)
    {
        int totalSkillDamage = player.skillDamage * Damage;
        monster.hp -= totalSkillDamage;
    }
}

public class IceSpear : Skill
{
    public static IceSpear instance;

    public static IceSpear Instance()
    {
        if (instance == null)
        {
            instance = new IceSpear("아이스 스피어", 25, 10);
        }
        return instance;
    }

    public IceSpear (string name, int damage, int speed) : base (name, damage, speed)
    {
        Name = name;
        Damage = damage;
        Speed = speed;
    }

    public override void UseSkill(PlayerStatus player, MonsterData monster)
    {
        int totalSkillDamage = player.skillDamage * Damage;
        monster.hp -= totalSkillDamage;
    }
}

public class DoubleAttack : Skill
{
    public static DoubleAttack instance;

    public static DoubleAttack Instance()
    {
        if (instance == null)
        {
            instance = new DoubleAttack("더블어택", 2, 0);
        }
        return instance;
    }

    public DoubleAttack (string name, int damage, int speed) : base (name, damage, speed)
    {
        Name = name;
        Damage = damage;
        Speed = speed;
    }

    public override void UseSkill(PlayerStatus player, MonsterData monster)
    {
        int totalSkillDamage = player.damage * Damage;
        monster.hp -= totalSkillDamage;
    }
}


