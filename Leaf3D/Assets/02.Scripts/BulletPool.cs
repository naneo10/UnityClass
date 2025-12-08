using System.Collections.Generic;
using UnityEngine;

/*
public class Base : MonoBehaviour
{
    protected Animator anim;
    protected Coroutine fireCoroutine;
    protected WaitForSeconds waitForFire;

    protected virtual void Awake()
    {
        anim = GetComponent<Animator>();
        waitForFire = GetComponent<WaitForSeconds>();
    }
}
*/

public class BulletPool : MonoBehaviour
{
    public static BulletPool Instance { get; private set; }

    [Header("Pool Setting")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private int initialSize = 60; //생성해 둘 총알 갯수

    [SerializeField] private Transform bulletParent; //부모 오브젝트

    private Queue<Bullet> pool = new Queue<Bullet>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        //부모 설정 안했을 시 자기 자신으로
        if (bulletParent == null)
        {
            bulletParent = this.transform;
        }

        for (int i = 0; i < initialSize; i++)
        {
            CreateNewBullet();
        }
    }

    //총알을 만들어서 풀에 넣는 메서드
    private Bullet CreateNewBullet()
    {
        GameObject obj = Instantiate(bulletPrefab, bulletParent);

        obj.SetActive(false);

        Bullet bullet = obj.GetComponent<Bullet>();

        if(bullet == null)
        {
            bullet = obj.AddComponent<Bullet>();
        }

        //불렛이 되돌아올 풀을 지정


        //연결
        bullet.SetPool(this);

        pool.Enqueue(bullet);

        return bullet;
    }

    //플레이어가 호출하는 메서드
    public void SpawnBullet(Vector3 position, Quaternion rotation, float lifeTime)
    {
        if (pool.Count == 0)
        {
            CreateNewBullet();
        }
        Bullet bullet = pool.Dequeue();

        //총알의 위치 / 회전 설정
        Transform t = bullet.transform;
        t.position = position;
        t.rotation = rotation;

        //활성화를 시키고 수명 초기화
        bullet.gameObject.SetActive(true);

        //수명 초기화(불렛에서 만들고 불러오자)
        bullet.Reuse(lifeTime);
    }

    //총알수명이 끝나면 호출하는 메서드
    public void ReturnBullet(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
        pool.Enqueue(bullet);
    }
}
