using System.Runtime.CompilerServices;
using UnityEngine;

/*
[매니저 클래스]
-게임 전체에서 공용으로 쓸 매니저들을 한군데 모아두는 정적 클래스
-여기서는 풀매니저 같은 매니저를 관리한다
-정적클래스이기 때문에 new 키워드로 만들지 않아도 된다
*/

public static class Managers //관리만 해주기에 MonoBehaviour이 없어도 된다
{
    //모든 매니저 오브젝트들의 부모 역할을 하는 빈 게임 오브젝트
    private static GameObject _root;

    //풀매니저
    private static PoolManager _pool;

    private static void Init()
    {
        if (_root == null)
        {
            //빈 게임 오브젝트 생성(@Managers으로)
            _root = new GameObject("@Managers");
            Object.DontDestroyOnLoad(_root);
        }
    }

    private static void CreateManager<T>(ref T manager, string name) where T : Component
    {
        if (manager == null)
        {
            Init(); //루트 만들고

            //새로운 게임 오브젝트 생성
            GameObject obj = new GameObject(name);

            //해당 오브젝트에 T 타입 매니저 컴포넌트 추가
            manager = obj.AddComponent<T>();

            Object.DontDestroyOnLoad(obj);

            //@Managers 밑으로 붙여서 계층 정리
            obj.transform.SetParent(_root.transform);
        }
    }

    /// <summary>
    /// 풀 매니저 접근자
    /// Managers.Pool
    /// </summary>
    public static PoolManager Pool
    {
        get //어디선가 한 번은 불러줘야 사용할 수 있다
        {
            CreateManager(ref _pool, "PoolManager");
            return _pool;
        }
    }
}
