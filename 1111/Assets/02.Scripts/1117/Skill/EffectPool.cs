using System.Collections.Generic;
using UnityEngine;

public class EffectPool
{
    //이펙트를 담아둘 큐 구조
    private Queue<GameObject> poolQueue = new Queue<GameObject>();
    private GameObject prefab; //풀에서 재사용할 프리팹
    private Transform parent;

    public EffectPool(GameObject prefab, int preloadCount, Transform parent)
    {
        this.prefab = prefab;
        this.parent = parent;

        //미리 preloadCount 개수만큼 만들어서 꺼내 쓰도록 큐에 넣어둔다.
        for (int i = 0; i < preloadCount; i++)
        {
            GameObject go = Object.Instantiate(prefab, parent);
            go.SetActive(false);
            poolQueue.Enqueue(go);
        }
    }

    public GameObject Get(Vector3 position, Quaternion rotation)
    {
        GameObject go;
        if (poolQueue.Count > 0)
        {
            go = poolQueue.Dequeue();
        }
        else
        {
            go = Object.Instantiate(prefab);
        }

        //위치랑 회전 설정
        go.transform.SetPositionAndRotation(position, rotation);

        //부모 밑으로 정리되도록 설정
        go.transform.SetParent(parent, true);

        //활성화 하고
        go.SetActive(true);

        //리턴
        return go;
    }

    public void ReturnPool(GameObject go)
    {
        //비활성화 하고
        go.SetActive(false);
        //매니저 밑으로 설정하고
        go.transform.SetParent(parent);
        //큐에 다시 넣자
        poolQueue.Enqueue(go);
    }
}