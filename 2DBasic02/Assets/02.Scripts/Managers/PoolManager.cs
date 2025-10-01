using System.Collections.Generic;
using UnityEngine;
/*
[디자인 패턴]
-소프트웨어 설계에서 자주 발생하는 문제를 해결하기 위해 정리해둔 재사용 가능한 설계 방법론
-코드 그 자체라는 거 보다 설계 아이디어 / 구조

[생성패턴]
:생성 디자인 패턴은 기존 코드의 유연성과 재사용을 증가시키는 객체를 생성하는 다양한 방법

[구조 패턴]

[행동 패턴]

[싱글톤 패턴]★★★★
:싱글톤은 클래스의 인스턴스가 반드시 하나만 있도록 하면서, 이 인스턴스가 전역접근 지점을 제공하는 디자인패턴
:프로그램 안에서 어떤 객체가 딱 하나만 존재하도록 보장하는 설계방법
>>장점
-객체가 하나만(게임 전체에서 점수매니저, 사운드 매니저 같은건 하나만 있으면 충분)
-여러 개 생기지 않으니 데이터 충돌을 막을 수 있다
-어디서든 접근이 가능하다
-불필요하게 메모리를 쓰지 않는다
>>단점
-전역변수 처럼 남용될 위험이 있음
-테스트 어려움
-단일책임 원칙을 어길 수 있다 유지보수가 어렵다. OCP
*/

/// <summary>
/// 여러 종류의 풀을 (ObjectPool) 프리팹 이름으로 모아서 관리하는 매니저
/// </summary>
public class PoolManager : MonoBehaviour
{
    //전역접근이 가능한 싱글톤 인스턴스
    public static PoolManager Instance { get; private set; }

    //key = string(프리팹 이름), value = object 타입 저장

    //new ObjectPool<Bullet>
    //new ObjectPool<Enemy>
    //제네릭 타입이 다른 ObjectPool<T>들을 한 딕셔너리에 모아둔다
    //단, 꺼낼때는 다시 캐스팅을 해줘야 한다
    private Dictionary<string, object> pools = new Dictionary<string, object>();

    private void Awake()
    {
        if (Instance == null) //인스턴스가 없으면
        {
            Instance = this; //내 자신을 싱글톤으로 등록
            DontDestroyOnLoad(gameObject); //씬이 바뀌어도 파괴되지 않도록 유지
        }
        else
        {
            Destroy(gameObject); //중복된 자신은 삭제
        }
    }

    /// <summary>
    /// 풀 등록
    /// </summary>
    /// <typeparam name="T">MonoBehaviour 파생타입</typeparam> 컴포넌트 타입
    /// <param name="prefab">복제에 사용할 프리팹</param>
    /// <param name="initCount">처음에 몇개?</param>
    /// <param name="parent">풀 Root를 둘 부모</param>
    //메서드 마다 제약을 걸어두는게 유연하다
    public void CreatePool<T>(T prefab, int initCount, Transform parent = null) where T : MonoBehaviour
    {
        if (prefab == null) return; //안전장치 : 프리팹이 없으면 아무것도 하지말고

        string key = prefab.name; //키는 프리팹 이름
        if (pools.ContainsKey(key)) return; //이미 같은 이름의 풀이 있으면 생성하지 않는다

        //프리팹 이름으로 새로운 풀을 딕셔너리에 등록해서 필요할 때 찾아 사용하기 위해
        pools.Add(key, new ObjectPool<T>(prefab, initCount, parent)); //새 풀을 만들어 딕셔너리에 등록
    }

    /// <summary>
    /// 풀에서 하나 꺼낸다
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="prefab"></param>
    /// <returns></returns>
    public T GetFromPool<T>(T prefab) where T : MonoBehaviour
    {
        if (prefab == null) return null;

        //등록할 때 썻던 프리팹 이름으로 풀을 찾음
        //이름으로 풀 찾기 시도
        if (!pools.TryGetValue(prefab.name, out var box)) //box의 타입은 object
        {
            return null; //등록되지 않은 프리팹이면 null을 반환
        }

        //private Dictionary<string, object> pools = new Dictionary<string, object>();
        //object 타입 자리로 들어가면서 업캐스팅이 된다
        //object는 Dequeue가 없다 애초에 존재하지 않는다 그래서 캐스팅이 필요하다

        //object로 저장된 풀을 원래 제네릭 타입으로 캐스팅 ★★★
        var pool = box as ObjectPool<T>;

        if(pool != null)
        {
            //성공했다면 Dequeue()로 하나 꺼내서 활성화된 채로 반환
            return pool.Dequeue();
        }
        else
        {
            return null;
        }
    }

    /// <summary>
    /// 사용 완료한 인스턴스를 풀로 되돌리는 메서드
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="instance"></param>
    public void ReturnPool<T>(T instance) where T : MonoBehaviour
    {
        if (instance == null) return; //안전장치

        if (!pools.TryGetValue(instance.gameObject.name, out var box))
        {
            //어느 풀에도 속하지 않는다면 그냥 제거 / 관리하지 않는 객체라면 파괴
            Destroy(instance.gameObject);
            return;
        }

        var pool = box as ObjectPool<T>; //캐스팅

        if (pool != null)
        {
            pool.Enqueue(instance);
        }
    }
}
