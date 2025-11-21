using System.Collections;
using UnityEngine;

public class ArrayBasic : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyObj;

    void Start()
    {
        for (int i = 0; i < enemyObj.Length; i++)
        {
            Debug.Log(enemyObj[i].name);
        }
    }

    void Update()
    {
        
    }
}
