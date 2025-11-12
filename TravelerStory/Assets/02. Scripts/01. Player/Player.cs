using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    #region field
    public static Player Instance;

    [Header("이동")]
    private float moveSpeed = 5.0f;
    private float inputX;
    private float inputY;

    [Header("UI/Status")]
    [SerializeField] private Image hpImage;
    [SerializeField] private Image mpImage;
    [SerializeField] private TextMeshProUGUI hpText;
    [SerializeField] private TextMeshProUGUI mpText;

    [SerializeField] private Image smallHpImage;

    //컴포넌트
    private Rigidbody2D rb;
    public SpriteRenderer sr;
    private Animator anim;
    #endregion

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        CurrentStatusText();

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void Update()
    {
        inputX = Input.GetAxisRaw("Horizontal");
        inputY = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        if (!InteractionManager.Instance.changeScene)
        {
            Move();
            Direction();
        }
    }

    #region method
    private void Move()
    {
        rb.velocity = new Vector2(inputX, inputY).normalized * moveSpeed;
        if (Mathf.Abs(inputX) > 0.1f || Mathf.Abs(inputY) > 0.1f)
        {
            anim.SetBool("Move", true);
        }
        else if (Mathf.Abs(inputX) < 0.1f || Mathf.Abs(inputY) < 0.1f)
        {
            anim.SetBool("Move", false);
        }
    }

    private void Direction()
    {
        if (inputX < 0)
        {
            sr.flipX = true;
        }
        else if (inputX > 0)
        {
            sr.flipX = false;
        }
    }

    public void ChangeBarAmount()
    {
        hpImage.fillAmount = PlayerStatus.instance.hp / PlayerStatus.instance.MaxHp;
        smallHpImage.fillAmount = PlayerStatus.instance.hp / PlayerStatus.instance.MaxHp;
        mpImage.fillAmount = PlayerStatus.instance.mp / PlayerStatus.instance.MaxMp;
    }

    public void CurrentStatusText()
    {
        if (hpText == null && mpText == null) return;
        hpText.text = "" + ($"{PlayerStatus.Instance().hp} / {PlayerStatus.Instance().MaxHp}");
        mpText.text = "" + ($"{PlayerStatus.Instance().mp} / {PlayerStatus.Instance().MaxMp}");
    }

    void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, LoadSceneMode mode)
    {
        //GameObject.Find : https://codeposting.tistory.com/entry/Unity-%EC%9C%A0%EB%8B%88%ED%8B%B0-%EA%B2%8C%EC%9E%84%EC%98%A4%EB%B8%8C%EC%A0%9D%ED%8A%B8-transform-%EB%B0%A9%EB%B2%95-GameObject-find
        GameObject hpObject = GameObject.Find("HpText");
        GameObject mpObject = GameObject.Find("MpText");
        GameObject hpImage = GameObject.Find("HP");
        GameObject mpImage = GameObject.Find("MP");
        GameObject smallHpImage = GameObject.Find("SmallHp");

        if (hpObject != null) hpText = hpObject.GetComponent<TextMeshProUGUI>();
        if (mpObject != null) mpText = mpObject.GetComponent<TextMeshProUGUI>();
        if (hpImage != null) this.hpImage = hpImage.GetComponent<Image>();
        if (mpImage != null) this.mpImage = mpImage.GetComponent<Image>();
        if (smallHpImage != null) this.smallHpImage = smallHpImage.GetComponent<Image>();

        CurrentStatusText();
    }
    #endregion
}
