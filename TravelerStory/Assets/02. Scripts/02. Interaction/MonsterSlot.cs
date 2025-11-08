using UnityEngine;

public class MonsterSlot : MonoBehaviour
{
    #region
    public MonsterData monsterData;
    [SerializeField] private RectTransform interactionIcon;
    #endregion

    #region
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;
        interactionIcon.gameObject.SetActive(true);

        InteractionManager.Instance.AddMonsterInRange(this);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;

        //씬 이동 후 Exit 오류 방지
        if (interactionIcon == null || interactionIcon.gameObject == null) return;
        
        interactionIcon.gameObject.SetActive(false);

        if (InteractionManager.Instance != null)
        {
            InteractionManager.Instance.RemoveMonsterInRange(this);
        }
        else if (InteractionManager.Instance == null)
        {
            //싱글톤 제대로 작동하는지 확인용
            Debug.Log($"현재 인터렉션 메니저 : {InteractionManager.Instance}");
        }
    }
    #endregion
}
