using UnityEngine;

public class MonsterEnCounter : MonoBehaviour
{
    #region
    [SerializeField] private GameObject monster;
    [SerializeField] private RectTransform interactionIcon;

    private bool rangeIn;
    #endregion

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    #region
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;

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
