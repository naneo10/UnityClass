using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("UI")]
    [SerializeField] private GameObject panel;
    [SerializeField] private TextMeshProUGUI messageText;
    [SerializeField] private Button saveButton;
    [SerializeField] private Button cancleButton;

    [Header("toast")]
    [SerializeField] private CanvasGroup toast;
    [SerializeField] private TextMeshProUGUI toastText;
    [SerializeField] private float toastDuration = 1.5f;

    private string checkPointId;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        if (panel != null) panel.SetActive(false);

        //세이브 버튼 이벤트
        if (saveButton != null) saveButton.onClick.AddListener(OnClickSave);

        //취소 버튼 이벤트
        if (cancleButton != null) cancleButton.onClick.AddListener(Hide);
    }
    void OnClickSave()
    {
        if (!string.IsNullOrEmpty(checkPointId))
        {
            //게임메니저 요청하자
            if (GameManager.Instance != null)
            {
                GameManager.Instance.SaveCheckPoint(checkPointId);
                //메시지 출력
                StartCoroutine(ShowToast("complete"));
            }
        }
        Hide();
    }

    //패널 활성화
    public void Show(string checkPointId)
    {
        this.checkPointId = checkPointId;

        if (messageText != null)
        {
            messageText.text = "save?";
        }
        if (panel != null)
        {
            panel.SetActive(true);
        }
    }

    //패널 숨김
    public void Hide()
    {
        checkPointId = null;

        if (panel != null)
        {
            panel.SetActive(false);
        }
    }

    IEnumerator ShowToast(string msg)
    {
        if (toastText != null)
        {
            toastText.text = msg;
        }
        if (toastText == null) yield break;

        float time = 0.0f;

        //Fade in
        while (time < 0.2f)
        {
            time += Time.deltaTime;
            toast.alpha = Mathf.Lerp(0.0f, 1.0f, time / 0.2f);
            yield return null;
        }

        //Wait
        yield return new WaitForSeconds(toastDuration);

        //Fade out
        time = 0.0f;

        while (time < 0.25f)
        {
            time += Time.deltaTime;
            toast.alpha = Mathf.Lerp(1.0f, 0.0f, time / 0.25f);
            yield return null;
        }
    }
}
