using TMPro;
using UnityEngine;

public class Gold : MonoBehaviour
{
    #region field
    private Player player;

    [Header("Gold Text")]
    [SerializeField] private TextMeshProUGUI goldCount;
    #endregion

    #region method
    private void AddGold(int gold)
    {
        player.gold += gold;

        goldCount.text = player.gold.ToString();
    }

    private void SubtractGold(int gold)
    {
        if (player.gold < gold)
        {
            print("골드가 부족합니다");
        }

        player.gold -= gold;

        goldCount.text = player.gold.ToString();
    }
    #endregion
}
