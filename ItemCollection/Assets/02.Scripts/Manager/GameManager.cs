using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

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
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

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

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, LoadSceneMode mode)
    {
        #region memo
        /*
        활성화된 오브젝트를 찾아서 들어가자 : https://jeong57.tistory.com/97
        gameObject를 SetActive = false인 경우 스크립트도 같이 꺼진다
        TextMeshProUGUI 스크립트 접근 : https://zetezz.tistory.com/entry/%EC%9C%A0%EB%8B%88%ED%8B%B0-TextMeshPro-%ED%95%A8%EC%88%98%EB%A5%BC-%EC%86%8C%EC%8A%A4%EC%97%90%EC%84%9C-%EC%A0%91%EA%B7%BC%ED%95%98%EB%8A%94-%EB%B0%A9%EB%B2%95
        */
        #endregion
        //scripts
        itemPick = FindObjectOfType<ItemPick>(true);

        //UI/TypoGraphic
        time = GameObject.Find("Canvas")
            .transform.Find("Time")
            .gameObject.GetComponent<TextMeshProUGUI>();
        point = GameObject.Find("Canvas")
            .transform.Find("Point")
            .gameObject.GetComponent<TextMeshProUGUI>();

        //UI/Clear
        clearUI = GameObject.Find("Panel");
        clearTime = GameObject.Find("Canvas")
            .transform.Find("Panel/ClearTime")
            .gameObject.GetComponent<TextMeshProUGUI>();
        bestClearTime = GameObject.Find("Canvas")
            .transform.Find("Panel/BestClearTime")
            .gameObject.GetComponent<TextMeshProUGUI>();
        restartButton = GameObject.Find("Canvas")
            .transform.Find("Panel/Restart")
            .gameObject.GetComponent<Button>();
        exitButton = GameObject.Find("Canvas")
            .transform.Find("Panel/Exit")
            .gameObject.GetComponent<Button>();
    }

    private void ReStart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void Exit()
    {
#if UNITY_EDITOR //게임종료 : https://m.blog.naver.com/os2dr/221536765981
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
