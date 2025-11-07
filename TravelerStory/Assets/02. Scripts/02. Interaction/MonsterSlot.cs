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
        if (interactionIcon.gameObject != null)
        {
            interactionIcon.gameObject.SetActive(false);
        }

        InteractionManager.Instance.RemoveMonsterInRange(this);
    }
    #endregion
}
