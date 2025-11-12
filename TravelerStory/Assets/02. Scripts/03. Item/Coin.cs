using UnityEngine;

public class Coin : MonoBehaviour
{
    Rigidbody2D rb;
    float randomJumpForce;
    int coinValue;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        Jump();
        Value();
    }

    private void Jump()
    {
        randomJumpForce = Random.Range(2f, 5f); //YÃà ·£´ý
        Vector2 jumpVelocity = Vector2.up * randomJumpForce;
        jumpVelocity.x = Random.Range(-1f, 1f); //XÃà ·£´ý

        //ForceMode : https://coding-shop.tistory.com/316
        rb.AddForce(jumpVelocity, ForceMode2D.Impulse);
    }

    private void Value()
    {
        coinValue = Random.Range(150, 800); //°¡Ä¡ ·£´ý

        Gold.Instance.AddGold(coinValue); //µå¶ø°ú µ¿½Ã¿¡ È¹µæ
    }
}
