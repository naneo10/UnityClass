using UnityEngine;

public class MonsterEnCounter : MonoBehaviour
{
    #region
    [SerializeField] public Monster monster;
    [SerializeField] private RectTransform interactionIcon;

    public bool monsterRangeIn = false;
    #endregion

    #region
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;

        monsterRangeIn = true;

        interactionIcon.gameObject.SetActive(true);

        monster.TriggerCheck();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;
        //씬 이동 후 Exit 오류 방지
        if (this == null) return;

        monsterRangeIn = false;

        interactionIcon.gameObject.SetActive(false);
    }
    #endregion
}
