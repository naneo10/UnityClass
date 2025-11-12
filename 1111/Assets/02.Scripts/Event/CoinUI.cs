using UnityEngine;

public class CoinUI : MonoBehaviour
{
    [SerializeField] private PlayerCoin player;

    private void OnEnable()
    {
        if (player != null)
        {
            player.onGetCoin += UpdateUI;
        }
    }

    private void OnDisable()
    {
        if (player != null)
        {
            player.onGetCoin -= UpdateUI;
        }
    }

    private void UpdateUI()
    {
        Debug.Log("UI에 코인수를 갱신");
    }
}
