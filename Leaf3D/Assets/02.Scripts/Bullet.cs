using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10.0f;

    private float remainingLife;
    private bool isActive;

    //돌아갈 풀을 참조
    private BulletPool pool;

    //풀에서 총알을 만들때 한 번 연결해주자
    public void SetPool(BulletPool pool)
    {
        this.pool = pool;
    }

    //풀에서 다시 꺼내 쓸 때 마다 호출
    public void Reuse(float lifeTime)
    {
        remainingLife = lifeTime;
        isActive = true;
    }

    private void OnDisable()
    {
        isActive = false;
        remainingLife = 0.0f;
    }

    void Update()
    {
        if (!isActive) return;

        transform.position += transform.forward * moveSpeed * Time.deltaTime;

        remainingLife -= Time.deltaTime;
        
        //총알의 수명이 다됬으면 풀로 돌리자
        if (remainingLife <= 0.0f)
        {
            isActive = false;

            if (pool != null)
            {
                pool.ReturnBullet(this);
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }
}
