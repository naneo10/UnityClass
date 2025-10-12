using UnityEngine;

public class ClearCheck : MonoBehaviour
{
    Collider2D goleTouch;
    

    [Header("체크박스")]
    public Transform ClearBox;
    [SerializeField] private float ClearRadius = 0.7f;
    [SerializeField] private LayerMask clearMask;

    private bool checkBox = false;

    void Update()
    {
        RealClear();
    }

    public void RealClear()
    {
        goleTouch = Physics2D.OverlapCircle(ClearBox.position, ClearRadius, clearMask);
        GameManager gameManager = FindObjectOfType<GameManager>();

        if(goleTouch != null)
        {
            if(gameManager.coin == 20)
            {
                gameObject.SetActive(false);
                gameManager.GameClear();
            }
            else
            {
                return;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(ClearBox.position, ClearRadius);
    }
}
