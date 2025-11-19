using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : MonoBehaviour
{
    private T prefab; //복제 및 활용하고자 하는 프리팹
    private Queue<T> pool = new Queue<T>();

    public Transform Root { get; private set; } //복제한 프리팹을 담아둘 폴더

    public ObjectPool(T prefab, int initCount, Transform parent = null)
    {
        this.prefab = prefab;
        Root = new GameObject($"{prefab.name}_pool").transform; //prefab.name_pool 자식으로 할당

        if (parent != null)
        {
            Root.SetParent(parent, false);
        }

        for (int i = 0; i < initCount; i++)
        {
            var inst = UnityEngine.Object.Instantiate(prefab, Root);
            inst.name = prefab.name;
            inst.gameObject.SetActive(false);
            pool.Enqueue(inst); //Queue에 넣는 자체 내장매서드
        }
    }

    public T Dequeue()
    {
        if (pool.Count == 0) return null;

        var inst = pool.Dequeue();
        inst.gameObject.SetActive(true);
        return inst;
    }

    public void Equeue(T instance) //넣고자 하는 오브젝트 인자로 받고
    {
        if (instance == null) return; //방어코드

        instance.gameObject.SetActive(false); //비활성화
        pool.Enqueue(instance); //담기 내장매서드
    }
}
