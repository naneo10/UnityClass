using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5.0f;

    private Rigidbody2D rb;

    private float inputX;

    public LayerMask target;

    private float rayDistance = 2.0f;

    private int itemCount = 0;

    //레이방향
    private Vector2 rayDir = Vector2.zero;

    private SpriteRenderer lastColor;

    private Color changeColor = new Color(1.0f, 0.0f, 0.0f, 1.0f); //RGBA

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //float moveX = Input.GetAxisRaw("Horizontal"); // -1,0,1 //GetAxis는 자동으로 보관을 해줘서 움직임이 부드럽다
        //float moveY = Input.GetAxisRaw("Vertical");

        //Vector2 moveDir = new Vector2(moveX, moveY);

        //transform.Translate(moveDir * moveSpeed * Time.deltaTime);

        inputX = Input.GetAxisRaw("Horizontal");
        RayDirection();
        RayInteract();
    }

    private void FixedUpdate()
    {
        //Vector2 vec = rb.velocity;
        //vec.x = inputX * moveSpeed;
        Move();
    }

    void Move()
    {
        Vector2 moveDir = new Vector2(inputX, rb.velocity.y).normalized; //inputY는 선언하지 않았으므로 기본값으로 기입
        rb.velocity = moveDir * moveSpeed;
    }

    void RayInteract()
    {
        if (rayDir == Vector2.zero) return;
        ResetColor();

        //전방에 5초간 레이발사
        RaycastHit2D hit = Physics2D.Raycast(transform.position, rayDir, rayDistance, target);

        if(hit.collider != null)
        {
            Debug.Log($"히트된 오브젝트 : {hit.collider.name}");

            if (hit.collider.CompareTag("Item"))
            {
                ChangeColor(hit.collider);
                ItemPick(hit.collider);
            }
        }

        Debug.DrawRay(transform.position, rayDir * rayDistance, Color.red);
    }

    //레이 방향
    void RayDirection()
    {
        if (inputX < 0) rayDir = Vector2.left;
        else if (inputX > 0) rayDir = Vector2.right;
    }

    //다시 흰색으로
    void ResetColor()
    {
        if (lastColor == null) return;
        lastColor.color = Color.white;
        lastColor = null;
    }

    //색 바꾸기
    void ChangeColor (Collider2D col)
    {
        var spriteRenderer = col.GetComponent<SpriteRenderer>();
        if (spriteRenderer == null) return;
        lastColor = spriteRenderer;
        spriteRenderer.color = changeColor;
    }

    void ItemPick(Collider2D col)
    {
        if (!Input.GetKeyDown(KeyCode.E)) return;

        var item = col.GetComponent<Item>();

        string log;

        if (item != null)
        {
            log = item.itemName;
        }
        else
        {
            log = col.name;
        }
        itemCount++;
        Debug.Log($"획득 : {log} / 총 {itemCount}개");
        Destroy(col.gameObject);
    }
}
