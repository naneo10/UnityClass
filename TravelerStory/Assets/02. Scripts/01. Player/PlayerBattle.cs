using UnityEngine;

public class PlayerBattle : MonoBehaviour
{
    #region field
    private Player player;
    #endregion

    void Awake()
    {
        player = Player.Instance; //애니메이션
    }

    #region method
    public void Attack(PlayerStatus player, MonsterData monster)
    {
        int totalDamage = player.damage;
    }

    public void UseSkill(PlayerStatus player, MonsterData monster)
    {

    }

    public void UseItem()
    {

    }

    public void Win()
    {

    }
    #endregion
}
