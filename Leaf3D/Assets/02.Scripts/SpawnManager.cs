using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private Monster monster;

    void Start()
    {
        StartCoroutine(SpawnCo());
    }

    IEnumerator SpawnCo()
    {
        float xPos = Random.Range(-3.5f, 3.5f);
        float zPos = Random.Range(35.0f, 55.0f);

        Instantiate(monster, new Vector3(xPos, 0.0f, zPos), Quaternion.Euler(0.0f, -180.0f, 0.0f));

        yield return new WaitForSeconds(Random.Range(1.0f, 3.0f));

        StartCoroutine(SpawnCo());
    }
}
