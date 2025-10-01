using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerA : MonoBehaviour
{
    [Header("이동/점프")]
    private float moveSpeed = 5.0f;
    private float jumpForce = 9.0f;

    [Header("바닥체크")]
    public Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.3f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private GameObject bulletPrefab;

    //[Header("방향체크")]
    //public Transform frontDir;
    //[SerializeField] private float frontDirRadius = 0.3f;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    private bool isJumpPressed;
    private bool isGrounded;
    private float inputX;
    private bool isShoted;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        inputX = Input.GetAxisRaw("Horizontal");
        if (inputX != 0)
        {
            if (inputX < 0)
            {
                spriteRenderer.flipX = true;
            }
            else
            {
                spriteRenderer.flipX = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            isJumpPressed = true;
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            isShoted = true;
        }
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        rb.velocity = new Vector2(inputX * moveSpeed, rb.velocity.y);

        if (isJumpPressed && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
        isJumpPressed = false;

        if (isShoted)
        {
            if (spriteRenderer.flipX)
            {
                InputShot(Vector2.left, 8.0f);
                isShoted = false;
            }
            else
            {
                InputShot(Vector2.right, 8.0f);
                isShoted = false;
            }
        }
    }

    private void InputShot (Vector2 pos, float speed)
    {
        GameObject tan = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        if (tan.TryGetComponent<BulletA>(out BulletA bulletA))
        {
            bulletA.Shot(pos, speed);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        //Gizmos.DrawWireSphere(frontDir.position, frontDirRadius);
    }
}
