using UnityEngine;

public class PlayerStatus
{
    #region field
    public float hp = 300;
    public float mp = 200;
    public int damage = 20;
    public int skillDamage = 0;
    public int speed = 10;
    #endregion

    #region mathod
    public void ModifyHP(float hp)
    {
        this.hp += hp;
    }

    public void ModifyMP(float mp)
    {
        this.mp += mp;
    }
    #endregion
}