using UnityEngine;

public abstract class Enemy : MonoBehaviour, IDamageble
{
    //이동속도
    [SerializeField] private float speed; //속도
    [SerializeField] private int health; //체력
    [SerializeField] private Sprite[] sprites; //스프라이트 배열

    protected SpriteRenderer spriteRenderer;
    protected Rigidbody2D rigid;

    protected virtual void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
    }

    public virtual void TakeDamage(int damage)
    {
        health -= damage;
        spriteRenderer.sprite = sprites[1]; //다른 스프라이트로 바꿔버림

        //0.1초 뒤에 해당 메서드를 실행해라
        Invoke(nameof(ReturnSprite), 0.1f); //nameof : 매서드의 닉네임을 문자열로 안전하게 변환

        //체력이 0보다 같거나 작으면
        if (health <= 0)
        {
            //안녕
            Die();

        }
    }

    //원래대로 다시 돌리자
    private void ReturnSprite()
    {
        spriteRenderer.sprite = sprites[0]; //원래 스프라이트로
    }

    protected virtual void Die()
    {
        gameObject.SetActive(false);
    }

    //
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //부딪힌 오브젝트에 불렛컴포넌트가 있는지 확인
        if(collision.TryGetComponent<Bullet>(out Bullet bullet))
        {
            //총알에 맞았을 때 처리 샐행
            OnBulletHit(bullet);
        }
    }

    protected virtual void OnBulletHit(Bullet bullet)
    {
        TakeDamage(bullet.Damage); //총알이 가진 데미지 만큼 채력감소
    }
}
