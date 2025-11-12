using UnityEngine;

public class CoinVFX : MonoBehaviour
{
    [SerializeField] private PlayerCoin player;

    private void OnEnable()
    {
        if(player != null)
        {
            player.onGetCoin += CoinEffect;
        }
    }

    private void OnDisable()
    {
        if (player != null)
        {
            player.onGetCoin -= CoinEffect;
        }
    }

    private void CoinEffect()
    {
        Debug.Log("코인이 반짝 거리는 효과");
    }
}
