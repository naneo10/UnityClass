using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    public ItemPick itemPick;

    [Header("UI/TyphoGraphic")]
    [SerializeField] private TextMeshProUGUI time;
    [SerializeField] private TextMeshProUGUI point;

    [Header("UI/Clear")]
    [SerializeField] private GameObject clearUI;
    [SerializeField] private TextMeshProUGUI clearTime;
    [SerializeField] private TextMeshProUGUI bestClearTime;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button exitButton;

    private float playTime;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(instance);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);

        playTime = 0.0f;
    }

    void Start()
    {
        if (clearUI != null) clearUI.SetActive(false);
        if (restartButton != null) restartButton.onClick.AddListener(ReStart);
        if (exitButton != null) exitButton.onClick.AddListener(Exit);
    }

    void Update()
    {
        playTime += Time.deltaTime;
        time.text = "Time : " + (int)playTime;

        RefreshPoint();
    }

    private void ReStart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    private void RefreshPoint()
    {
        point.text = "Point : " + itemPick.currentPoint.ToString() + " / 20";
    }

    public void Clear()
    {
        clearUI.SetActive(true);

        clearTime.text = time.text;

        float bestTime = PlayerPrefs.GetFloat("bestClearTime");

        if (playTime < bestTime)
        {
            bestTime = playTime;
            PlayerPrefs.SetFloat("bestClearTime", bestTime);
        }
        else if (bestTime == 0.0f)
        {
            bestTime = playTime;
            PlayerPrefs.SetFloat("bestClearTime", bestTime);
        }

        bestClearTime.text = "Best Clear Time : " + (int)bestTime;
    }
}
