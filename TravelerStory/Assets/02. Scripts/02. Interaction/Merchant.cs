using UnityEngine;

public class Merchant : MonoBehaviour
{
    #region
    [SerializeField] private RectTransform interactionIcon;

    private bool rangeIn = false;
    #endregion

    void Awake()
    {
        
    }

    void Update()
    {
        
    }

    #region method
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return; //플레이어가 아니라면 리턴

        rangeIn = true;

        interactionIcon.gameObject.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;

        rangeIn = false;

        interactionIcon.gameObject.SetActive(false);
    }
    #endregion
}
