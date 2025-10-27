using UnityEngine;

public class PlayerStatus
{
    public int hp = 300;
    public int mp = 200;
    public int damage = 20;
    public int skillDamage = 0;
    public int speed = 10;
}

public class Player : MonoBehaviour
{
    #region field
    [Header("이동")]
    private float moveSpeed = 5.0f;
    private float inputX;
    private float inputY;

    //컴포넌트
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator anim;
    #endregion

    void Awake()
    {
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
    #endregion
}
