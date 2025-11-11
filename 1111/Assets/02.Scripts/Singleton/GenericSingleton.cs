using UnityEngine;

/*
[제네릭 싱글톤]
-기존에는 클래스마다 같은 싱글톤 코드를 작성해야 했음
-제네릭을 사용하면 공통 로직을 한 번만 작성하고 여러 클래스가 이 코드를 상속 받아
    싱글톤으로 동작하게 만들 수 있음

^예시
public class SoundManager : GemericSingleton<SoundManager> {}
public class GameManager : GemericSingleton<GameManager> {}

SoundManager.instance 전역 접근이 가능해진다
*/

public class GenericSingleton<T> : MonoBehaviour where T : Component
{
    private static T inst;

    public static T Instance
    {
        get
        {
            //인스턴스가 없으면 생성하자
            if (inst == null)
            {
                //씬 안에서 같은 타입의 컴포넌트를 탐색
                inst = (T)FindFirstObjectByType(typeof(T));

                //그래도 없으면 새로운 게임오브젝트를 만들어서 생성
                if (inst == null)
                {
                    GameObject go = new GameObject();
                    inst = go.AddComponent<T>();

                    go.name = typeof(T).Name;

                    DontDestroyOnLoad(go);
                }
            }
            return inst;
        }
    }

    public virtual void Awake()
    {
        if (inst == null)
        {
            inst = this as T;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
