using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerControllerA : MonoBehaviour
{
    private Rigidbody playerRigid;
    [SerializeField]private float moveSpeed = 5.0f;

    [SerializeField] private GameObject bulletPerfab;

    private float spawnRateMin = 1.0f;
    private float spawnRateMax = 3.0f;

    private float spawnRate;

    private float horizontal;
    private float vertical;

    private void Awake()
    {
        playerRigid = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        spawnRate = Random.Range(spawnRateMin, spawnRateMax);

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

        transform.rotation = Quaternion.Lerp(currentRotation, targetRotation, Time.deltaTime * moveSpeed);
    }

    private IEnumerator SpawnBulletCo()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnRate);

            GameObject go = Instantiate(bulletPerfab, transform.position, Quaternion.identity);

            if (go.TryGetComponent<BulletB>(out BulletB bulletb))
            {
                bulletb.Shot(transform.forward, 18.0f);
            }

            spawnRate = Random.Range(spawnRateMin, spawnRateMax);
        }
    }

    public void Die()
    {
        gameObject.SetActive(false);

        GameManagerA gameManagerA = FindObjectOfType<GameManagerA>();
        gameManagerA.EndGame();
    }
}
