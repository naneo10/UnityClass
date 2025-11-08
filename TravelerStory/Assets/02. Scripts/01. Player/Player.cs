using UnityEngine;
using UnityEngine.UI;

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

    //컴포넌트
    private Rigidbody2D rb;
    private SpriteRenderer sr;
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
    }

    void Update()
    {
        inputX = Input.GetAxisRaw("Horizontal");
        inputY = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        Move();
        Direction();
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

    public void ChangeHPBarAmount(int hp, int MaxHp)
    {
        hpImage.fillAmount = hp / MaxHp;
    }

    public void ChangeMPBarAmount(int mp, int MaxMp)
    {
        mpImage.fillAmount = mp / MaxMp;
    }
    #endregion
}
