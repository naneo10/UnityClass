using UnityEngine;

public class CoinSFX : MonoBehaviour
{
    [SerializeField] private PlayerCoin player;

    private void OnEnable()
    {
        if (player != null)
        {
            player.onGetCoin += CoinSound;
        }
    }

    private void OnDisable()
    {
        if (player != null)
        {
            player.onGetCoin -= CoinSound;
        }
    }

    private void CoinSound()
    {
        Debug.Log("코인을 얻는 효과음 재생");
    }
}
