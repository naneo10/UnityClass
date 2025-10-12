using System.Reflection;
using UnityEngine;

public class CoinPick : MonoBehaviour
{
    #region config
    [Header("자석")]
    [SerializeField] private float magnetRadius = 1.0f;
    [SerializeField] private float pullSpeed = 5.0f;
    [SerializeField] private float collectDistance = 0.2f;

    [Header("코인 레이어")]
    [SerializeField] LayerMask itemLayer;
    #endregion

    void Update()
    {
        Hits();
    }

    #region Method
    public void Hits()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(
            transform.position,
            magnetRadius,
            itemLayer
            );

        for (int i = 0; i < hits.Length; i++)
        {
            if (!hits[i].TryGetComponent<Coin>(out Coin coin))
            {
                continue;
            }

            float distance = Vector2.Distance(transform.position, coin.transform.position);

            if (distance <= collectDistance)
            {
                GameManager gameManager = FindObjectOfType<GameManager>();
                gameManager.IncreaseCoin();
                coin.Collect();
            }

            coin.transform.position = Vector2.Lerp(
                coin.transform.position,
                transform.position,
                pullSpeed * Time.deltaTime
                );
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0.5f, 0.9f, 0.0f, 0.5f);
        Gizmos.DrawWireSphere(transform.position, magnetRadius);
    }
    #endregion
}
