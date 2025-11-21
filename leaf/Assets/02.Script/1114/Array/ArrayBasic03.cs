using System.Security.Cryptography;
using UnityEngine;

public class ArrayBasic03 : MonoBehaviour
{
    [SerializeField] private Transform[] patrolPoints;

    private int index = 0;

    void Update()
    {
        transform.position = Vector3.MoveTowards(
            transform.position, 
            patrolPoints[index].position, 
            3.0f * Time.deltaTime
            );

        if (Vector3.Distance(transform.position, patrolPoints[index].position) < 0.2f) //0.2보다 작으면 도착한걸로 취급
        {
            index = Random.Range(0, patrolPoints.Length);
        }
    }
}
