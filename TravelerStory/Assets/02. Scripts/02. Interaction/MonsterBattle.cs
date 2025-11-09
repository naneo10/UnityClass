using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MonsterBattle : MonoBehaviour
{
    #region field
    [SerializeField] private MonsterData monsterData;
    [SerializeField] private Image smallMonsterHpImage;
    #endregion

    #region mehod
    public void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, LoadSceneMode mode)
    {
        GameObject hpBar = GameObject.Find("SmallMonsterHp");
        if (hpBar != null) smallMonsterHpImage = hpBar.GetComponent<Image>();
    }

    public void ChangeHPBarAmount()
    {
        smallMonsterHpImage.fillAmount = monsterData.hp / monsterData.maxHp;
    }
    #endregion
}
