using UnityEngine;

public class PlayerStatus
{
    public static PlayerStatus instance;

    #region field
    public static PlayerStatus Instance()
    {
        if (instance == null)
        {
            instance = new PlayerStatus();
        }
        return instance;
    }

    public int hp = 300;
    public int mp = 200;
    public int damage = 20;
    public int skillDamage = 0;
    public int defense = 0;
    public int speed = 10;

    public int MaxHp = 300;
    public int MaxMp = 200;

    public int Gold = 1000;
    #endregion

    public void Awake()
    {
        instance = this;
    }

    public void Update()
    {
        Player.Instance.ChangeHPBarAmount(hp, MaxHp);
        Player.Instance.ChangeMPBarAmount(mp, MaxMp);
        Player.Instance.CurrentStatusText();
    }

    #region mathod
    public void ModifyHP(int hp)
    {
        if (this.hp >= MaxHp)
        {
            Debug.Log("최대 체력이므로 먹을 수 없음");
            InteractionManager.Instance.useItem = false;
            return;
        }
        else if (this.hp < MaxHp)
        {
            InteractionManager.Instance.useItem = true;
            this.hp += hp;

            if (this.hp + hp >= MaxHp)
            {
                this.hp = 300;
            }
        }

    }

    public void ModifyMP(int mp)
    {
        if (this.mp >= MaxMp)
        {
            Debug.Log("최대 마력이므로 먹을 수 없음");
            InteractionManager.Instance.useItem = false;
            return;
        }
        else if (this.mp < MaxMp)
        {
            InteractionManager.Instance.useItem = true;
            this.mp += mp;

            if (this.mp + mp >= MaxMp)
            {
                this.mp = 200;
            }
        }
    }
    #endregion
}