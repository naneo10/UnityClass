using TMPro;
using UnityEngine;

public class Gold : MonoBehaviour
{
    #region field
    public static Gold Instance { get; private set; }

    [Header("Gold Text")]
    [SerializeField] private TextMeshProUGUI goldCount;
    #endregion

    private void Awake()
    {
        if (Instance != null || Instance != this)
        {
            Destroy(Instance);
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        Fresh();
    }

    #region method
    private void Fresh()
    {
        goldCount.text = "" + PlayerStatus.Instance().Gold.ToString();
    }
    public void AddGold(int gold)
    {
        PlayerStatus.Instance().Gold += gold;

        goldCount.text = PlayerStatus.Instance().Gold.ToString();
    }

    public void SubtractGold(int gold)
    {
        if (PlayerStatus.Instance().Gold < gold)
        {
            Debug.Log("골드가 부족합니다");
            return;
        }

        PlayerStatus.Instance().Gold -= gold;

        goldCount.text = PlayerStatus.Instance().Gold.ToString();
    }
    #endregion
}
