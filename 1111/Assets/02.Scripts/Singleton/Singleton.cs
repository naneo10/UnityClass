using UnityEngine;

/*
생명주기가 길다
게임시작 시 무조건 시작
심플
장점:단순 그 자체
단점:불필요한 메모리를 사용하게 될 수 있다.
*/
public class Singleton : MonoBehaviour
{
    public static Singleton Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

}

//지연
//실제로 LazySingleton.Instance가 호출될 떄 까지 아무것도 생성이 안됨 > 불필요한 초기화 방지
//단점:처음 접근할 때 약간의 비용발생 (Find + new GameObject)
public class LazySingleton : MonoBehaviour
{
    private static LazySingleton _instance;

    public static LazySingleton Instance
    {
        get
        {
            if (_instance = null)
            {
                //인스턴스를 셋업하고
                SetupInstance();
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private static void SetupInstance()
    {
        _instance  = FindObjectOfType<LazySingleton>();
        if (_instance == null)
        {
            var go = new GameObject("LazySington");
            _instance = go.AddComponent<LazySingleton>();
            DontDestroyOnLoad(go);
        }
    }
}
