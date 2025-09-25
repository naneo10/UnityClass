using System.Collections;
using UnityEngine;

public class EnemyA : MonoBehaviour
{
    //�Ѿ� ������(�ν����Ϳ��� �Ҵ�)
    [SerializeField] private GameObject bulletPerfab;

    //Ÿ���� �� Ʈ������
    private Transform target;

    //�ּ�, �ִ� ���� �ð� ����
    private float spawnRateMin = 2.0f;
    private float spawnRateMax = 4.0f;

    //���� ���� ����
    private float spawnRate;

    void Start()
    {
        //ó�� ������ �ð��� �������� ����
        spawnRate = Random.Range(spawnRateMin, spawnRateMax);
        //�ش� ���� �ִ� ��� ������Ʈ�� �˻��ؼ� ã�ƿ���
        target = FindObjectOfType<PlayerControllerA>()?.transform; //Null ���� ������

        StartCoroutine(SpawnBulletCo());
    }

    private void Update()
    {
        transform.RotateAround(target.position, Vector3.up, 8f * Time.deltaTime);
    }

    private IEnumerator SpawnBulletCo()
    {
        while (true)
        {
            //���� ������ spawnRate��ŭ ���
            yield return new WaitForSeconds(spawnRate);

            //1.�Ѿ� ����
            GameObject go = Instantiate(bulletPerfab, transform.position, Quaternion.identity);

            //2.��ǥ���� ���
            Vector3 dir = (target.position - transform.position).normalized;

            //3.Ÿ���� �ٶ󺸵��� ����
            go.transform.LookAt(target);

            //4.�߻�
            if (go.TryGetComponent<BulletA>(out BulletA bullet))
            {
                bullet.Shot(dir, 8.0f);
            }

            //5.���� ���� ������ �������� ����
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
