
using UnityEngine;
public class ItemPick : MonoBehaviour
{
    [Header("자석")]
    [SerializeField] private float magnetRadius = 3.0f;
    [SerializeField] private float pullSpeed = 8.0f;
    [SerializeField] private float collectDistance = 0.2f; //수집 판정 거리

    [Header("아이템 레이어")]
    [SerializeField] LayerMask itemLayer;

    private void Update()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(
            transform.position,
            magnetRadius,
            itemLayer
            );

        for(int i = 0; i < hits.Length; i++)
        {
            if (!hits[i].TryGetComponent<CItem>(out CItem item))
            {
                continue;
            }
            //플레이어와 아이템 사이의 거리계산
            float distance = Vector2.Distance(transform.position, item.transform.position);

            //만약 거리 안이면 획득하자
            if (distance <=collectDistance)
            {
                item.Collect();
            }

            //이동
            item.transform.position = Vector2.Lerp(
                item.transform.position,
                transform.position,
                pullSpeed * Time.deltaTime
                );
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1.0f, 0.9f, 0.0f, 0.7f);
        Gizmos.DrawWireSphere(transform.position, magnetRadius);
    }
}
