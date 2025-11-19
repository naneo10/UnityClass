using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
ObjectPool<T>
즉, 아래 오브젝트 풀 클래스는 특정타입에 맞춰서 재사용 가능한 클래스
Ex)
:ObjectPool<Bullet> bulletPool;
:ObjectPool<Enemy> enemyPool;
:ObjectPool<Effect> enemyPool;

where T : MonoBehaviour
:제네릭<T>에 아무 타입이나 들어가면 안되고, 반드시 MonoBehaviour를 상속한 타입만 들어가도록 제한
:만약에 T에 int, string 게임오브젝트를 쓸 수 없음
:즉, 풀에 들어올 타입은 반드시 유니티 컴포넌트(스크립트)여야 한다는 규칙을 강제

:ObjectPool는 제네릭 클래스 이고 T라는 타입으로 만들수 있는데 단,
그 타입은 반드시 MonoBehaviour를 상속해야 한다

ObjectPool<T> : MonoBehaviour 이렇게 쓰면 컴포넌트가 된다. 무조건 오브젝트를 만들어서 사용해야된다
:데이터 구조로서 사용이된다 gameObject 같은걸 쓰기 어려워진다
*/
public class ObjectPool<T> where T : MonoBehaviour //T : MonoBehaviour로 제약을 건다
{
    private T prefab; //복사할 원본 프리팹
    private Queue<T> pool = new Queue<T>();
    /// <summary>
    /// 부모 오브젝트로 활용하고 생성되는건 자식으로 받는 컨테이너 역할 즉, 정리하는 역할
    /// 풀을 담아둘 부모 오브젝트(컨테이너 역할)
    /// :유니티 계층에서 보기 쉽게 하기 위해
    /// :주의:부모 오브젝트가 비활성화되면 자식도 강제로 비활성화 되므로 항상 켜져 있어야 한다
    /// </summary>
    public Transform Root { get; private set; }

    /// <summary>
    /// 생성자
    /// </summary>
    /// <param name="prefab">복제할 원본 프리팹</param> //매개변수들을 설명할 때 사용하는 용도
    /// <param name="initCount">처음에 몇 개를 미리 만들지 정해두는 용도</param>
    /// <param name="parent">Root를 어떤 부모 아래에 둘지, 아무것도 전달하지 않으면 기본 값 null</param>

    public ObjectPool(T prefab, int initCount, Transform parent = null)
    {
        this.prefab = prefab;
        //풀 컨테이너 생성(Root) -> 이름은 "[프리팹이름]_pool"
        Root = new GameObject($"{prefab.name}_pool").transform;

        if(parent != null)
        {
            //부모가 없으면 최상단에 두겠다
            Root.SetParent(parent, false);
        }

        //처음에 지정한 갯수 만큼 미리 만들어서 큐에 넣어둔다
        for(int i = 0; i < initCount; i++)
        {
            //Root의 자식으로 생성
            var inst = Object.Instantiate(prefab, Root);
            inst.name = prefab.name; //이름
            inst.gameObject.SetActive(false); //꺼진 상태로 대기
            pool.Enqueue(inst); //Queue에 넣는다
        }
    }

    /// <summary>
    /// 꺼내서 사용
    /// </summary>
    public T Dequeue()
    {
        if (pool.Count == 0) return null;

        var inst = pool.Dequeue(); //큐에서 하나 빼고
        inst.gameObject.SetActive(true); //켜서 사용 (활성화)
        return inst;
    }

    /// <summary>
    /// 사용을 마친 오브젝트를 다시 pool에 넣자
    /// </summary>
    /// <param name="inst"></param>
    public void Enqueue(T instance)
    {
        if (instance == null) return;

        instance.gameObject.SetActive(false); //끄기
        pool.Enqueue(instance); //담기
    }
}
