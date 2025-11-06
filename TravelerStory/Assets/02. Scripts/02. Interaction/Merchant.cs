using UnityEngine;

public class Merchant : MonoBehaviour
{
    #region field
    [SerializeField] private RectTransform interactionIcon;

    private bool rangeIn;
    #endregion

    #region method
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return; //플레이어가 아니라면 리턴

        rangeIn = true;
        InteractionManager.Instance.IsNear(rangeIn);

        interactionIcon.gameObject.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;

        rangeIn = false;
        InteractionManager.Instance.IsNear(rangeIn);

        interactionIcon.gameObject.SetActive(false);
        InteractionManager.Instance.store.SetActive(false);
    }
    #endregion
}
