using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerControllerA : MonoBehaviour
{
    private Rigidbody playerRigid;
    [SerializeField]private float moveSpeed = 5.0f;

    [SerializeField] private GameObject bulletPerfab;

    private float spawnRateMin = 0.1f;
    private float spawnRateMax = 0.3f;

    private float spawnRate;

    private float horizontal;
    private float vertical;

    //Ãß°¡
    private int fanCount = 3;
    private float fanDeg = 8.0f;

    private int TypeNum;

    private void Awake()
    {
        playerRigid = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        spawnRate = Random.Range(spawnRateMin, spawnRateMax);
        TypeNum = Random.Range(1, 3);


        StartCoroutine(SpawnBulletCo());
    }

    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
    }

    private void FixedUpdate()
    {
        PlayerMove();
    }

    void PlayerMove()
    {
        Vector3 newVelocity = new Vector3(horizontal * moveSpeed, 0.0f, vertical * moveSpeed);
        playerRigid.velocity = Vector3.Lerp(playerRigid.velocity, newVelocity, Time.deltaTime * 10.0f);

        Quaternion currentRotation = transform.rotation;
        Quaternion targetRotation = Quaternion.LookRotation(newVelocity);

        if (horizontal == 0 && vertical == 0) return;

        transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, Time.deltaTime * moveSpeed);
    }

    private IEnumerator SpawnBulletCo()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnRate);
            switch(TypeNum)
            {
                case 1:
                    {
                        SpawnShot(transform.forward, 15.0f);
                    }
                    break;
                case 2:
                    {
                        FanShot(transform.forward, 15.0f);
                    }
                    break;
            }

            spawnRate = Random.Range(spawnRateMin, spawnRateMax);
            TypeNum = Random.Range(1, 3);
        }
    }

    private void SpawnShot(Vector3 pos, float speed)
    {
        GameObject go = Instantiate(bulletPerfab, transform.position, Quaternion.identity);

        if (go.TryGetComponent<BulletB>(out BulletB bulletb))
        {
            bulletb.Shot(pos, speed);
        }
    }

    private void FanShot(Vector3 pos, float speed)
    {
        int half = fanCount / 2;
        for (int i = -half; i <= half; i++)
        {
            float angle = i * fanDeg;

            Vector3 position = Quaternion.Euler(0, angle, 0) * pos;
            SpawnShot(position, speed);
        }
    }
    public void Die()
    {
        gameObject.SetActive(false);

        GameManagerA gameManagerA = FindObjectOfType<GameManagerA>();
        gameManagerA.LoseGame();
    }
}
